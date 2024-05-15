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

        List<BookWithAuthorAndPublisherDTO> GetAllBooks(string? filterOn = null, string?
       filterQuery = null);
        BookWithAuthorAndPublisherDTO GetBookById(int id);
        AddBookRequestDTO AddBook(AddBookRequestDTO addBookRequestDTO);
        AddBookRequestDTO? UpdateBookById(int id, AddBookRequestDTO bookDTO);
        Books? DeleteBookById(int id);


    }
}
