using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBackend.Models
{
    public class CartItem
    {
        [Key]
        public int CartItemId { get; set; }

        [ForeignKey("Cart")]
        public int CartId { get; set; }
        public Cart? Cart { get; set; }

        [ForeignKey("Movie")]
        public int MovieId { get; set; }
        public Movie? Movie { get; set; }

        public int Quantity { get; set; }
    }

}
