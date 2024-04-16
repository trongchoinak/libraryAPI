using libraryAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace libraryAPI.Data
{
    public class libraryAPIDbcontext : DbContext
    {
        public libraryAPIDbcontext(DbContextOptions<libraryAPIDbcontext> options) : base(options)
        {
        }
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            // Define relationship between books and authors
            builder.Entity<Books>()
                .HasOne(x => x.publishers)
                .WithMany(x => x.books);


            builder.Entity<Books_Authors>()
             .HasOne(x => x.books)
             .WithMany(x => x.books_Authors);

            builder.Entity<Books_Authors>()
                .HasOne(x => x.authors)
                .WithMany(x => x.books_Authors);

        }
    }
}
