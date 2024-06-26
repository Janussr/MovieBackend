namespace Movies.Core.Dto
{
    public class CreateMovieDto
    {
        public string MovieId { get; set; } = null!;

        public string? Title { get; set; }

        public int? ReleaseYear { get; set; }

        public string? Runtime { get; set; }

        public string? Genre { get; set; }

        public string? Summary { get; set; }

        public string? Directors { get; set; }

        public string? Actors { get; set; }


        public string? Publishers { get; set; }

        public float? Page { get; set; }

        public string? Poster { get; set; }

        public float? Price { get; set; }
    }
}
