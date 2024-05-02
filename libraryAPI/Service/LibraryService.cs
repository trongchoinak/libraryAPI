using libraryAPI.Data;
using libraryAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;
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
        public async Task<List<Authors>> GetAuthors()
        {
            try
            {
                return await _db.Authors.ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<Authors> GetAuthorID(int  AuthorID, bool includeBooks)
        {
            try
            {
                if (includeBooks) // books should be included
                {
                    return await _db.Authors.Include(b => b.AuthorID)
                        .FirstOrDefaultAsync(i => i.AuthorID == AuthorID);
                }

                // Books should be excluded
                return await _db.Authors.FindAsync(AuthorID);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Authors> AddAuthorAsync(Authors author)
        {
            try
            {
                await _db.Authors.AddAsync(author);
                await _db.SaveChangesAsync();
                return await _db.Authors.FindAsync(author.AuthorID); // Auto ID from DB
            }
            catch (Exception ex)
            {
                return null; // An error occured
            }
        }

        public async Task<Authors> UpdateAuthorAsync(Authors author)
        {
            try
            {
                _db.Entry(author).State = EntityState.Modified;
                await _db.SaveChangesAsync();

                return author;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<(bool, string)> DeleteAuthorAsync(Authors author)
        {
            try
            {
                var dbAuthor = await _db.Authors.FindAsync(author.AuthorID);

                if (dbAuthor == null)
                {
                    return (false, "Author could not be found");
                }

                _db.Authors.Remove(author);
                await _db.SaveChangesAsync();

                return (true, "Author got deleted.");
            }
            catch (Exception ex)
            {
                return (false, $"An error occured. Error Message: {ex.Message}");
            }
        }
    }
}
