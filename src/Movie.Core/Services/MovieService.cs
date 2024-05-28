using AutoMapper;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movies.Repository.Entities;

namespace Movies.Core.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly MovieDbContext _context;
    private readonly ILogger<MovieService> _logger;

    public MovieService(IMapper mapper, MovieDbContext context, ILogger<MovieService> logger)
    {
        _mapper = mapper;
        _context = context;
        _logger = logger;
    }

    public async Task<List<MovieDto>> GetMovies()
    {
        List<MovieEntity> movies;
        try
        {
            _logger.LogInformation("Attempting to retrieve movies.");
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



    public async Task<MovieDto> GetById(int id)
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



}