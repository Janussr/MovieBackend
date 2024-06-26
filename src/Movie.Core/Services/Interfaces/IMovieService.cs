using Movies.Core.Dto;

namespace Movies.Core.Services.Interfaces;

public interface IMovieService
{
    Task<List<MovieDto>> GetMovies();

    Task<MovieDto> GetMovieById(int id);

    Task<CreateMovieDto> CreateMovie(CreateMovieDto movie);

    Task<bool> UpdateMovie(MovieDto movieDto);

    Task<bool> DeleteMovie(int id);
}