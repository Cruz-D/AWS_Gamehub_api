using System.ComponentModel.DataAnnotations;

namespace gamehub_API.Infrastructure.Models
{
    public class User
    {
        [Key]
        public int id { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public bool? isVerified { get; set; }
    }
}
