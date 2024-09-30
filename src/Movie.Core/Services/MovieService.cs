using AutoMapper;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Repository.Entities;

namespace Movies.Core.Services;

public class MovieService(IMapper mapper, MovieDbContext context, ILogger<MovieService> logger) : IMovieService
{
    private readonly IMapper _mapper = mapper;
    private readonly MovieDbContext _context = context;
    private readonly ILogger<MovieService> _logger = logger;

    public async Task<CreateMovieDto> CreateMovie(CreateMovieDto movieDto)
    {
        try
        {
            // Map the DTO to the Movie entity
            var movie = _mapper.Map<MovieEntity>(movieDto);

            // Add the new movie to the context
            _context.Movies.Add(movie);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            // Map the Movie entity back to the DTO
            return _mapper.Map<CreateMovieDto>(movie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating a new movie.");
            throw;
        }
    }


    public async Task<List<MovieDto>> GetMovies()
    {
        List<MovieEntity> movies;
        try
        {
            movies = await _context.Movies.ToListAsync();
            _logger.LogInformation($"Retrieved {movies.Count} movies successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the movies."); // Log at the Error level
            throw; // Re-throwing the exception to let it be handled further up the call stack.
        }
        return _mapper.Map<List<MovieDto>>(movies);
    }



    public async Task<MovieDto> GetMovieById(int id)
    {
        try
        {
            var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == id);
            return _mapper.Map<MovieDto>(movie);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while getting the movie with ID: {MovieId}", id);
            throw;
        }
    }


    public async Task<bool> UpdateMovie(MovieDto movieDto)
    {
        try
        {
            // Retrieve the existing movie from the database
            var existingMovie = await _context.Movies.FindAsync(movieDto.Id);
            if (existingMovie == null)
            {
                return false; 
            }

            // Update the movie's properties with the values from the DTO
            _mapper.Map(movieDto, existingMovie);

            // Save the changes to the database
            await _context.SaveChangesAsync();

            return true; // Update successful
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the movie with ID: {MovieId}", movieDto.Id);
            return false; // Update failed
        }
    }



    public async Task<bool> DeleteMovie(int id)
    {
        try
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie != null)
            {
                _context.Movies.Remove(movie);
                await _context.SaveChangesAsync();
                return true; // Deletion successful
            }
            return false; // Movie not found
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the movie with ID: {MovieId}", id);
            return false; // Deletion failed
        }
    }

   


}

