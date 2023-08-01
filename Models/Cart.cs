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
        public User User { get; set; } // Assuming you have a User class for the 'Users' table

        [ForeignKey("Movie")]
        public int id { get; set; }
        public Movie Movie { get; set; } // Assuming you have a Movie class for the 'Movies' table

        public int Quantity { get; set; }


    }
}
