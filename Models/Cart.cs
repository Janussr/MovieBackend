using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBackend.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User? User { get; set; }

        public required ICollection<CartItem> CartItems { get; set; }
    }

}
