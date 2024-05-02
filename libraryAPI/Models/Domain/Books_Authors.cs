using System.ComponentModel.DataAnnotations;
namespace libraryAPI.Models.Domain
{
    public class Books_Authors
    {
        [Key]
        public int Books_AuthorsID { get; set; }
        // khóa ngoại 
        public int BookID { get; set; }
        public int AuthorID { get; set; }
        public Books books { get; set; }

        public Authors authors { get; set; }
    }
}
