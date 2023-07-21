using AutoMapper;
using MovieBackend.DbConnector;
using MovieBackend.DTOs;

namespace MovieBackend.Services
{
    public class MovieService
    {
        // Instance variables
        private readonly UserContext _context;
        private readonly IMapper _mapper;


        //Constructor
        public MovieService(UserContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }


        //Get all Users
        public List<MovieDTO> GetAllMovies()
        {
            var movies = _context.Movies.ToList();
            var movieDTO = _mapper.Map<List<MovieDTO>>(movies);
            return movieDTO;
        }

    }
}
