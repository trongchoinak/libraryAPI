using libraryAPI.Data;
using libraryAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using libraryAPI.Models.DTO;
namespace libraryAPI.Service
{
    public class LibraryService : iLibraryService
    {
        private readonly libraryAPIDbcontext _db;
        public LibraryService(libraryAPIDbcontext db)
        {
            _db = db;
        }

        public List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string?
filterQuery = null)
        {
            var allBooks = _db.Books.Select(Books => new BookWithAuthorAndPublisherDTO()
            {
                Id = Books.BookID,
                Title = Books.title,
                Description = Books.description,
                IsRead = Books.Isread,
                DateRead = Books.Isread ? Books.DateRead.Value : null,
                Rate = Books.Isread ? Books.Rate : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublisherName = Books.publishers.publishersName,
                AuthorName = Books.books_Authors.Select(n => n. authors.Fullname).ToList()
            }).AsQueryable();
            //filtering
            if (string.IsNullOrWhiteSpace(filterOn) == false &&
           string.IsNullOrWhiteSpace(filterQuery) == false)
            {
                if (filterOn.Equals("title", StringComparison.OrdinalIgnoreCase))
                {
                    allBooks = allBooks.Where(x => x.Title.Contains(filterQuery));
                }
            }
            return allBooks.ToList();
        }
        public BookWithAuthorAndPublisherDTO GetBookById(int id)
        {
            var bookWithDomain = _db.Books.Where(n => n.BookID == id);
            //Map Domain Model to DTOs
            var bookWithIdDTO = bookWithDomain.Select(book => new
           BookWithAuthorAndPublisherDTO()
            {
                Id = book.BookID,
                Title = book.title,
                Description = book.description,
                IsRead = book.Isread,
                DateRead = book.DateRead,
                Rate = book.Rate,
                Genre = book.Genre,
                CoverUrl = book.CoverUrl,
                PublisherName = book.publishers.publishersName,
                AuthorName = book.books_Authors.Select(n => n.authors.Fullname).ToList()
            }).FirstOrDefault();
            return bookWithIdDTO;
        }
        public AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO)
        {
            //map DTO to Domain Model
            var bookDomainModel = new Books
            {
                title = addBookRequestDTO.Title,
                description = addBookRequestDTO.Description,
                Isread = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                publishersId = addBookRequestDTO.PublisherId
            };
            //Use Domain Model to add Book 
            _db.Books.Add(bookDomainModel);
            _db.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Books_Authors()
                {
                    BookID = bookDomainModel.BookID,
                    AuthorID = id
                };
                _db.books_Authors.Add(_book_author);
                _db.SaveChanges();
            }
            return addBookRequestDTO;
        }
        public AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO)
        {
            var bookDomain = _db.Books.FirstOrDefault(n => n.BookID == id);
            if (bookDomain != null)
            {
                bookDomain.title = bookDTO.Title;
                bookDomain.description = bookDTO.Description;
                bookDomain.Isread = bookDTO.IsRead;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.publishersId = bookDTO.PublisherId;
                _db.SaveChanges();
            }
            var authorDomain = _db.books_Authors.Where(a => a.BookID == id).ToList();
            if (authorDomain != null)
            {
                _db.books_Authors.RemoveRange(authorDomain);
                _db.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIds)
            {
                var _book_author = new Books_Authors()
                {
                    BookID = id,
                    AuthorID = authorid
                };
                _db.books_Authors.Add(_book_author);
                _db.SaveChanges();
            }
            return bookDTO;
        }
        public Books? DeleteBookById(int id)
        {
            var bookDomain = _db.Books.FirstOrDefault(n => n.BookID == id);
            if (bookDomain != null)
            {
                _db.Books.Remove(bookDomain);
                _db.SaveChanges();
            }
            return bookDomain;
        }
    
    public List<AuthorDTO> GellAllAuthors()
        {
            //Get Data From Database -Domain Model 
            var allAuthorsDomain = _db.Authors.ToList();
            //Map domain models to DTOs 
            var allAuthorDTO = new List<AuthorDTO>();
            foreach (var authorDomain in allAuthorsDomain)
            {
                allAuthorDTO.Add(new AuthorDTO()
                {
                    Id = authorDomain.AuthorID,
                    FullName = authorDomain.Fullname
                });
            }
            //return DTOs 
            return allAuthorDTO;
        }
        public AuthorNoIdDTO GetAuthorById(int id)
        {
            // get book Domain model from Db
            var authorWithIdDomain = _db.Authors.FirstOrDefault(x => x.AuthorID ==
           id);
            if (authorWithIdDomain == null)
            {
                return null;
            }
            //Map Domain Model to DTOs 
            var authorNoIdDTO = new AuthorNoIdDTO
            {
                FullName = authorWithIdDomain.Fullname,
            };
            return authorNoIdDTO;
        }
        public AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorDomainModel = new Authors
            {
                Fullname = addAuthorRequestDTO.FullName,
            };
            //Use Domain Model to create Author 
            _db.Authors.Add(authorDomainModel);
            _db.SaveChanges();
            return addAuthorRequestDTO;
        }
        public AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO)
        {
            var authorDomain = _db.Authors.FirstOrDefault(n => n.AuthorID == id);
            if (authorDomain != null)
            {
                authorDomain.Fullname = authorNoIdDTO.FullName;
                _db.SaveChanges();
            }
            return authorNoIdDTO;
        }
        public Authors? DeleteAuthorById(int id)
        {
            var authorDomain = _db.Authors.FirstOrDefault(n => n.AuthorID == id);
            if (authorDomain != null)
            {
                _db.Authors.Remove(authorDomain);
                _db.SaveChanges();
            }
            return null;
        }
    }
}
