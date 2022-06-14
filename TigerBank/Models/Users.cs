using System.ComponentModel.DataAnnotations;

namespace TigerBank.Models
{
    public class Users
    {
        [Key]
        public int userId { get; set; }

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Salt { get; set; } = string.Empty ;
    }
}
