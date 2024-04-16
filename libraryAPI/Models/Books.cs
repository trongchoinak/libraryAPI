using System.ComponentModel.DataAnnotations;

namespace libraryAPI.Models
{
    public class Books
    {
        [Key]
        public int BookID { get; set; }
        public string title { get; set; }

        public string description { get; set; }
        public  bool Isread { get; set; }
        public DateTime DateRead { get; set; }

        public int Rate { get; set; }
        public int Genre { get; set; }
        public string CoverUrl { get; set; }
        public DateTime DateAdded { get; set; }


        public ICollection<Books_Authors> books_Authors { get; set; }
        // khóa ngoại 
        public int publishersId { get; set; }

        public publishers publishers { get; set; }

    }
}
