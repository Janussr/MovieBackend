using System.ComponentModel.DataAnnotations;

namespace MovieBackend.DTOs
{
    public class UserDTO
    {

        public int UserId { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }
       
    }
}
