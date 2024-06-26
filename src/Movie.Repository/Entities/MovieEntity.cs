using System;
using System.Collections.Generic;

namespace Movies.Repository.Entities;



public partial class MovieEntity //I had to name the entity "MovieEntity" as the project is named "Movie" which complicated also having this entity be named "Movie"
{
    public int Id { get; set; }

    public string MovieId { get; set; } = null!;

    public string? Title { get; set; }

    public int? ReleaseYear { get; set; }

    public string? Runtime { get; set; }

    public string? Genre { get; set; }

    public float? Rating { get; set; }

    public string? Summary { get; set; }

    public string? Directors { get; set; }

    public string? Actors { get; set; }

    public decimal? LifetimeGrossInUsd { get; set; }

    public string? Reviews { get; set; }

    public string? ReviewScore { get; set; }

    public string? ReviewUser { get; set; }

    public string? Publishers { get; set; }

    public float? Page { get; set; }

    public string? Poster { get; set; }

    public float? Price { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
