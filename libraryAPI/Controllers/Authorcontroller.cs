using libraryAPI.Models.Domain;
using libraryAPI.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace libraryAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] 
        public class AuthorController : ControllerBase
        {
            private readonly iLibraryService _libraryService;

            public AuthorController(iLibraryService libraryService)
            {
                _libraryService = libraryService;
            }

            [HttpGet]
            [Authorize(Roles = "Read,Write")]
            public async Task<IActionResult> GetAuthors()
            {
                var authors = await _libraryService.GetAuthors();

                if (authors == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, "No authors in database");
                }

                return StatusCode(StatusCodes.Status200OK, authors);
            }

            [HttpGet("id")]

            public async Task<IActionResult> GetAuthor(int AuthorID, bool includeBooks = false)
            {
                Authors author = await _libraryService.GetAuthorID(AuthorID, includeBooks);

                if (author == null)
                {
                    return StatusCode(StatusCodes.Status204NoContent, $"No Author found for id: {AuthorID}");
                }
                return StatusCode(StatusCodes.Status200OK, author);
            }

            [HttpPost]
            public async Task<ActionResult<Authors>> AddAuthor(Authors author)
            {
                var dbAuthor = await _libraryService.AddAuthorAsync(author);

                if (dbAuthor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Fullname} could not be added.");
                }

                return CreatedAtAction("GetAuthor", new { id = author.AuthorID }, author);
            }

            [HttpPut("id")]
            public async Task<IActionResult> UpdateAuthor(int  id, Authors author)
            {
                if (id != author.AuthorID)
                {
                    return BadRequest();
                }

                Authors dbAuthor = await _libraryService.UpdateAuthorAsync(author);

                if (dbAuthor == null)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, $"{author.Fullname} could not be updated");
                }

                return NoContent();
            }

            [HttpDelete("id")]
            public async Task<IActionResult> DeleteAuthor(int AuthorID)
            {
                var author = await _libraryService.GetAuthorID(AuthorID, false);
                (bool status, string message) = await _libraryService.DeleteAuthorAsync(author);

                if (status == false)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, message);
                }

                return StatusCode(StatusCodes.Status200OK, author);
            }
        }
    }
