using Microsoft.AspNetCore.Mvc;
using libraryAPI.Service;
using libraryAPI.Models.Domain;
using libraryAPI.Data;
using libraryAPI.Models.DTO;
namespace libraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Bookscontroller : ControllerBase
    {
        protected readonly libraryAPIDbcontext _libraryAPIDbcontext;
        public Bookscontroller(libraryAPIDbcontext libraryAPIDbcontext)
        {
            this._libraryAPIDbcontext = libraryAPIDbcontext;
        }
        [HttpGet]
        public IActionResult GetallBooks()
        {
            var allBooksDomain = _libraryAPIDbcontext.Books;
            var allBooksDTO = allBooksDomain.Select(Books => new BookWithAuthorAndPublisherDTO()
            {
                Id = Books.BookID,
                Title = Books.title,
                Description = Books.description,
                IsRead = Books.Isread,
                DateRead = Books.Isread ? Books.DateRead : null,
                Rate = Books.Isread ? Books.Rate : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublisherName = Books.publishers.publishersName,
                AuthorName = Books.books_Authors.Select(n => n.authors.Fullname).ToList()
            }).ToList();
            return Ok(allBooksDTO);
        }
        [HttpGet]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var BookWithDomian = _libraryAPIDbcontext.Books.Where(n => n.BookID == id);
            if (BookWithDomian == null)
            {
                return NotFound();
            }
            var BookWithIdDTO = BookWithDomian.Select(Books => new BookWithAuthorAndPublisherDTO()
            {
                Id = Books.BookID,
                Title = Books.title,
                Description = Books.description,
                IsRead = Books.Isread,
                DateRead = Books.Isread ? Books.DateRead : null,
                Rate = Books.Isread ? Books.Rate : null,
                Genre = Books.Genre,
                CoverUrl = Books.CoverUrl,
                PublisherName = Books.publishers.publishersName,
                AuthorName = Books.books_Authors.Select(n => n.authors.Fullname).ToList()
            });
            return Ok(BookWithIdDTO);
        }
        [HttpPost("add-book")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            var bookDomainModel = new Books
            {
                title = addBookRequestDTO.Title ?? "Untitled",
                description = addBookRequestDTO.Description ?? "Untitled",
                Isread = addBookRequestDTO.IsRead,
                DateRead = addBookRequestDTO.DateRead,
                Rate = addBookRequestDTO.Rate,
                Genre = addBookRequestDTO.Genre,
                CoverUrl = addBookRequestDTO.CoverUrl,
                DateAdded = addBookRequestDTO.DateAdded,
                publishersId = addBookRequestDTO.PublisherId
            };
            _libraryAPIDbcontext.Books.Add(bookDomainModel);
            _libraryAPIDbcontext.SaveChanges();
            foreach (var id in addBookRequestDTO.AuthorIds)
            {
                var _book_author = new Books_Authors()
                {
                    BookID = bookDomainModel.BookID,
                    AuthorID = id
                };
                _libraryAPIDbcontext.books_Authors.Add(_book_author);
                _libraryAPIDbcontext.SaveChanges();
            }
            return Ok();
        }
        [HttpPut("update-book-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var bookDomain = _libraryAPIDbcontext.Books.FirstOrDefault(n => n.BookID == id);
            if (bookDomain != null)
            {
                bookDomain.title = bookDTO.Title ?? "Untitled";
                bookDomain.description = bookDTO.Description ?? "Untitled";
                bookDomain.Isread = bookDTO.IsRead;
                bookDomain.DateRead = bookDTO.DateRead;
                bookDomain.Rate = bookDTO.Rate;
                bookDomain.Genre = bookDTO.Genre;
                bookDomain.CoverUrl = bookDTO.CoverUrl;
                bookDomain.DateAdded = bookDTO.DateAdded;
                bookDomain.publishersId = bookDTO.PublisherId;
                _libraryAPIDbcontext.SaveChanges();
            }
            var authorDomain = _libraryAPIDbcontext.books_Authors.Where(a => a.BookID == id).ToList();
            if (authorDomain != null)
            {
                _libraryAPIDbcontext.books_Authors.RemoveRange(authorDomain);
                _libraryAPIDbcontext.SaveChanges();
            }
            foreach (var authorid in bookDTO.AuthorIds)
            {
                var _book_author = new Books_Authors()
                {
                    BookID = id,
                    AuthorID = authorid,
                };

                _libraryAPIDbcontext.books_Authors.Add(_book_author);
                _libraryAPIDbcontext.SaveChanges();
            }
            return Ok(bookDTO);
        }
        [HttpDelete("delete-book-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var bookDomain = _libraryAPIDbcontext.Books.FirstOrDefault(n => n.BookID == id);
            if (bookDomain != null)
            {
                _libraryAPIDbcontext.Books.Remove(bookDomain);
                _libraryAPIDbcontext.SaveChanges();
            }
            return Ok();
        }

    }
}
        
        /*
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBooks(Guid id)
        {
            Books book = await iLibraryService.GetBookID(id);

            if (book == null)
            {
                return StatusCode(StatusCodes.Status204NoContent, $"No book found for id: {id}");
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }
        [HttpPost]
        public async Task<ActionResult<Books>> AddBook(Books book)
        {
            var dbBook = await iLibraryService.CreateBooks(book);

            if (dbBook == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{book.title} could not be added.");
            }

            return CreatedAtAction("GetBook", new { id = book.BookID }, book);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBook(int  id, Books book)
        {
            if (id != book.BookID)
            {
                return BadRequest();
            }

            Books dbBook = await iLibraryService.UpdateBookAsync(book);

            if (dbBook == null)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"{book.title} could not be updated");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBook(Guid id)
        {
            var book = await iLibraryService.GetBookID(id);
            (bool status, string message) = await iLibraryService.DeleteBookAsync(book);

            if (status == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, message);
            }

            return StatusCode(StatusCodes.Status200OK, book);
        }
    }
}*/
