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



        // Method to add a movie to the user's cart
     //   public void AddMovieToCart(int userId, int movieId)
     //   {
     //       // Check if the user already has the movie in their cart
     //       var existingCartItem = _context.CartItems.FirstOrDefault(c => c.UserId == userId && c.MovieId == movieId);
     //
     //       if (existingCartItem != null)
     //       {
     //           // If the movie is already in the cart, increase the quantity
     //           existingCartItem.Quantity++;
     //       }
     //       else
     //       {
     //           // If the movie is not in the cart, create a new cart item
     //           var cartItem = new CartItems
     //           {
     //               UserId = userId,
     //               MovieId = movieId,
     //               Quantity = 1 // Set the initial quantity to 1
     //           };
     //
     //           _context.CartItems.Add(cartItem);
     //       }
     //
     //       _context.SaveChanges();
     //   }


















    }
}
