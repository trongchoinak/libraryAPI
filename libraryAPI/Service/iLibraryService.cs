using libraryAPI.Models.Domain;

namespace libraryAPI.Service
{
    public interface iLibraryService
    {
        Task<List<Authors>> GetAuthors(); // GET All Authors
        Task<Authors> GetAuthorID(int  AuthorID, bool includeBooks = false); // GET Single Author
        Task<Authors> AddAuthorAsync(Authors author); // POST New Author
        Task<Authors> UpdateAuthorAsync(Authors author); // PUT Author
        Task<(bool, string)> DeleteAuthorAsync(Authors author); // DELETE Author

        // Book Services
        Task<List<Books>> GetBooks(); // GET All Books
        Task<Books> GetBookID(Guid BookID); // Get Single Book
        Task<Books> CreateBooks(Books book); // POST New Book
        Task<Books> UpdateBookAsync(Books book); // PUT Book
        Task<(bool, string)> DeleteBookAsync(Books book); // DELETE Book


    }
}
