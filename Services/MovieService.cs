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


        //Get all Movies
        public List<MovieDTO> GetAllMovies()
        {
            var movies = _context.Movies.ToList();
            var movieDTO = _mapper.Map<List<MovieDTO>>(movies);

           

            return movieDTO;
        }

       public List<MovieDTO> GetAllMoviesOrderedByYearASC()
       {
           var movies = _context.Movies.OrderBy(movie => movie.ReleaseYear).ToList();
           var movieDTOs = _mapper.Map<List<MovieDTO>>(movies);
           return movieDTOs;
       }
      
       public List<MovieDTO> GetAllMoviesOrderedByYearDESC()
       {
           var movies = _context.Movies.OrderByDescending(movie => movie.ReleaseYear).ToList();
           var movieDTOs = _mapper.Map<List<MovieDTO>>(movies);
           return movieDTOs;
       }
    }
}
