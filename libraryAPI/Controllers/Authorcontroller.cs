using libraryAPI.Data;
using libraryAPI.Models.Domain;
using libraryAPI.Service;
using Microsoft.AspNetCore.Authorization;
using libraryAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace libraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
        public class AuthorController : ControllerBase
        {
            private readonly iLibraryService _libraryService;
        private readonly libraryAPIDbcontext _dbContext;
        public AuthorController(iLibraryService libraryService,libraryAPIDbcontext dbcontext)
            {
                _libraryService = libraryService;
            _dbContext = dbcontext;
            }

        [HttpGet("get-all-author")]
        public IActionResult GetAllAuthor()
        {
            var allAuthors = _libraryService.GellAllAuthors();
            return Ok(allAuthors);
        }
        [HttpGet("get-author-by-id/{id}")]
        public IActionResult GetAuthorById(int id)
        {
            var authorWithId = _libraryService.GetAuthorById(id);
            return Ok(authorWithId);
        }
        [HttpPost]
        public IActionResult AddAuthors([FromBody] AddAuthorRequestDTO addAuthorRequestDTO)
        {
            var authorAdd = _libraryService.AddAuthor(addAuthorRequestDTO);
            return Ok();
        }
        [HttpPut("update-author-by-id/{id}")]
        public IActionResult UpdateBookById(int id, [FromBody] AuthorNoIdDTO
       authorDTO)
        {
            var authorUpdate = _libraryService.UpdateAuthorById(id, authorDTO);
            return Ok(authorUpdate);
        }
        [HttpDelete("delete-author-by-id/{id}")]
        public IActionResult DeleteBookById(int id)
        {
            var authorDelete = _libraryService.DeleteAuthorById(id);
            return Ok();
        }
    }
    }
