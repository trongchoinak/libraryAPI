using Microsoft.AspNetCore.Mvc;
using libraryAPI.Service;
using libraryAPI.Models.Domain;
using libraryAPI.Data;
using libraryAPI.Models.DTO;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;
namespace libraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BooksController : ControllerBase
    {
        private readonly libraryAPIDbcontext _dbContext;
        private readonly iLibraryService iLibraryService;
        private readonly ILogger<BooksController> _logger;
        public BooksController(libraryAPIDbcontext dbContext, iLibraryService bookRepository, ILogger<BooksController> logger)
        {
            _dbContext = dbContext;
            iLibraryService = bookRepository;
            _logger = logger;
        }
        [HttpGet("get-all-books")]
        [Authorize(Roles = "Read")]
        public IActionResult GetAll([FromQuery] string? filterOn, [FromQuery] string? filterQuery)
        {
            _logger.LogInformation("GetAll Book Action method was invoked");
            _logger.LogWarning("This is a warning log");
            _logger.LogError("This is a error log");
            // su dung reposity pattern 
            var allBooks = iLibraryService.GetAllBooks(filterOn, filterQuery);
            _logger.LogInformation($"Finished GetAllBook request with data { JsonSerializer.Serialize(allBooks)}");
            return Ok(allBooks);
        }
        [HttpGet]
        [Authorize(Roles = "Read")]
        [Route("get-book-by-id/{id}")]
        public IActionResult GetBookById([FromRoute] int id)
        {
            var bookWithIdDTO = iLibraryService.GetBookById(id);
            return Ok(bookWithIdDTO);
        }
        [HttpPost]
        [Authorize(Roles = "Write")]
        public IActionResult AddBook([FromBody] AddBookRequestDTO addBookRequestDTO)
        {
            var bookAdd = iLibraryService.AddBook(addBookRequestDTO);
            return Ok(bookAdd);
        }
        [HttpPut("update-book-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult UpdateBookById(int id, [FromBody] AddBookRequestDTO bookDTO)
        {
            var updateBook = iLibraryService.UpdateBookById(id, bookDTO);
            return Ok(updateBook);
        }
        [HttpDelete("delete-book-by-id/{id}")]
        [Authorize(Roles = "Write")]
        public IActionResult DeleteBookById(int id)
        {
            var deleteBook = iLibraryService.DeleteBookById(id);
            return Ok(deleteBook);
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
