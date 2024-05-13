using libraryAPI.Models.Domain;
using libraryAPI.Models.DTO;

namespace libraryAPI.Service
{
    public interface iLibraryService
    {
        List<AuthorDTO> GellAllAuthors();
        AuthorNoIdDTO GetAuthorById(int id);
        AddAuthorRequestDTO AddAuthor(AddAuthorRequestDTO addAuthorRequestDTO);
        AuthorNoIdDTO UpdateAuthorById(int id, AuthorNoIdDTO authorNoIdDTO);
        Authors DeleteAuthorById(int id);
        // Book Services
        Task<List<Books>> GetBooks(); // GET All Books
        Task<Books> GetBookID(Guid BookID); // Get Single Book
        Task<Books> CreateBooks(Books book); // POST New Book
        Task<Books> UpdateBookAsync(Books book); // PUT Book
        Task<(bool, string)> DeleteBookAsync(Books book); // DELETE Book


    }
}
