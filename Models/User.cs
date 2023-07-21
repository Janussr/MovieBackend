using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class User
    {

        [Key]
        public int UserId { get; set; }

        [MaxLength(50)]
        public string Username { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [MaxLength(100)]
        public string Password { get; set; }
    }

}