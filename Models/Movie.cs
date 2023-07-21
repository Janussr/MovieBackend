using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class Movie
    {
        [Key]
        public int MovieId { get; set; }
        [MaxLength(50)]
        public string? Title { get; set; }
        [MaxLength(50)]
        public string? Genre { get; set; }
        [MaxLength(4)]
        public int Year { get; set; }
    }
}
