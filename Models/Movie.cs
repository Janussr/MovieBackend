using System.ComponentModel.DataAnnotations;

namespace MovieBackend.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }
        public string MovieId { get; set; }

        [MaxLength(255)]
        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }


        [MaxLength(255)]
        public string? Runtime { get; set; }

        [MaxLength(255)]
        public string? Genre { get; set; }

        public float? Rating { get; set; }

        [MaxLength(-1)]
        public string? Summary { get; set; }

        [MaxLength(255)]
        public string? Directors { get; set; }

        [MaxLength(255)]
        public string? Actors { get; set; }

        public decimal? LifetimeGrossInUSD { get; set; }

        public string? Reviews { get; set; }

        public string? ReviewScore { get; set; }
            
        public string? ReviewUser { get; set; }

        [MaxLength(255)]
        public string? Publishers { get; set; }

        [MaxLength(255)]
        public float? Page { get; set; }

        [MaxLength(-1)]
        public string? Poster { get; set; }

        public float? Price { get; set; }
    }

}
