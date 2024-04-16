using System.ComponentModel.DataAnnotations;

namespace libraryAPI.Models
{
    public class Authors
    {
        [Key]
        public int AuthorID { get; set; }
        public string Fullname { get; set; }

        public ICollection<Books_Authors> books_Authors { get; set; }
    }
}
