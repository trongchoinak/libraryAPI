using libraryAPI.Data;
using libraryAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
using libraryAPI.Models.DTO;
namespace libraryAPI.Service
{
    public class LibraryService:iLibraryService
    {
        private readonly libraryAPIDbcontext _db;
        public LibraryService(libraryAPIDbcontext db)
        {
            _db = db;
        }

        public async Task<List<Books>> GetBooks()
        {
            try
            {
                return await _db.Books.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Books> GetBookID(Guid BookID)
        {
            try
            {
                return await _db.Books.FindAsync(BookID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Books> CreateBooks(Books book)
        {
            try
            {
                await _db.Books.AddAsync(book);
                await _db.SaveChangesAsync();
                return await _db.Books.FindAsync(book.BookID);
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Books> UpdateBookAsync(Books book)
        {
            try
            {
                _db.Entry(book).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return book;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteBookAsync(Books book)
        {
            try
            {
                var dbBook = await _db.Books.FindAsync(book.BookID);

                if (dbBook == null)
                {
                    return (false, "Book could not be found.");
                }

                _db.Books.Remove(book);
                await _db.SaveChangesAsync();

                return (true, "Book got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
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
