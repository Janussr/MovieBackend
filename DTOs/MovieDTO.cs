using System.ComponentModel.DataAnnotations;

namespace MovieBackend.DTOs
{
    public class MovieDTO
    {
        public int Id { get; set; }
        public string MovieId { get; set; }

        public string? Title { get; set; }
        public int? ReleaseYear { get; set; }


        public string? Runtime { get; set; }

        public string? Genre { get; set; }

        public float? Rating { get; set; }

        public string? Summary { get; set; }

        public string? Directors { get; set; }

        public string? Actors { get; set; }

        public decimal? LifetimeGrossInUSD { get; set; }

       // public string? Reviews { get; set; }

       // public string? ReviewScore { get; set; }

       // public string? ReviewUser { get; set; }

        public string? Publishers { get; set; }

        public float? Page { get; set; }

        public string? Poster { get; set; }

        public float? Price { get; set; }
    }


}
