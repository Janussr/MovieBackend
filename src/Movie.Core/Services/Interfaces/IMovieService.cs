using Movies.Core.Dto;

namespace Movies.Core.Services.Interfaces;

public interface IMovieService
{
    Task<List<MovieDto>> GetMovies();
}