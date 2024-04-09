using AutoMapper;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Movies.Core.Services;

public class MovieService : IMovieService
{
    private readonly IMapper _mapper;
    private readonly MovieDbContext _context;

    public MovieService(IMapper mapper, MovieDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<List<MovieDto>> GetMovies()
    {
        var movies = await _context.Movies.ToListAsync();
        return _mapper.Map<List<MovieDto>>(movies);
    }


    public async Task<MovieDto> GetById(int id)
    {
        var movie = await _context.Movies.SingleOrDefaultAsync();
        return _mapper.Map<MovieDto>(movie);
    }


}