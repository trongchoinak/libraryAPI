using System.ComponentModel.DataAnnotations;
namespace libraryAPI.Models.Domain
{
    public class publishers
    {
        [Key]
        public int publishersId { get; set; }
        public string publishersName { get; set; }


        public ICollection<Books> books { get; set; }

    }
}
