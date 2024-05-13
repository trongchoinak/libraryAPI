using libraryAPI.Models.Domain;
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
        public DbSet<Books_Authors> books_Authors { get; set; }
        public DbSet<publishers> publishers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define relationship between books and authors
            modelBuilder.Entity<Books>()
                .HasOne(x => x.publishers)
                .WithMany(x => x.books);

            modelBuilder.Entity<Books_Authors>()
                .HasOne(x => x.books)
                .WithMany(x => x.books_Authors)
                  .HasForeignKey(bc => bc.BookID);

            modelBuilder.Entity<Books_Authors>()
                .HasOne(x => x.authors)
                .WithMany(x => x.books_Authors)
             .HasForeignKey(bc => bc.AuthorID   );
            // Seed data
            modelBuilder.Entity<Authors>().HasData(
                new Authors { AuthorID = 1, Fullname = "Nguyễn Văn A" },
                new Authors { AuthorID = 2, Fullname = "Trần Thị B" },
                new Authors { AuthorID = 3, Fullname = "Phạm Văn C" }
            );

            modelBuilder.Entity<publishers>().HasData(
                new publishers { publishersId = 1, publishersName = "Nhà xuất bản A" },
                new publishers { publishersId = 2, publishersName = "Nhà xuất bản B" }
            );

            modelBuilder.Entity<Books>().HasData(
                new Books
                {
                    BookID = 1,
                    title = "Sách 1",
                    description = "Mô tả sách 1",
                    Isread = true,
                    DateRead = DateTime.Now,
                    Rate = 5,
                    Genre = "Thể loại 1",
                    CoverUrl = "url_sach_1.jpg",
                    DateAdded = DateTime.Now,
                    publishersId = 1
                },
                new Books
                {
                    BookID = 2,
                    title = "Sách 2",
                    description = "Mô tả sách 2",
                    Isread = false,
                    DateRead = null,
                    Rate = 4,
                    Genre = "Thể loại 2",
                    CoverUrl = "url_sach_2.jpg",
                    DateAdded = DateTime.Now,
                    publishersId = 2
                }
            );

            modelBuilder.Entity<Books_Authors>().HasData(
                new Books_Authors { Books_AuthorsID = 1, BookID = 1, AuthorID = 1 },
                new Books_Authors { Books_AuthorsID = 2, BookID = 1, AuthorID = 2 },
                new Books_Authors { Books_AuthorsID = 3, BookID = 2, AuthorID = 3 }
            );
        }
    }

}
