using System.ComponentModel.DataAnnotations;

namespace gamehub_API.Models
{
    public class Videogame
    {
        [Key]
        public int Id { get; set; }
        public string title { get; set; }
        public string genre { get; set; }
        public string platform { get; set; }
        public int rating { get; set; }
        public string publisher { get; set; }
        public DateTime releaseDate { get; set; }
        public string status { get; set; }
        public string image { get; set; }
    }
}
