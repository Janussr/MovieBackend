using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MovieBackend.DbConnector;
using MovieBackend.DTOs;
using MovieBackend.Models;

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



        public void AddToCart(int userId, int movieId, int quantity)
        {
            // Retrieve the user's cart from the database using DTOs
            var cartDTO = _context.Carts.Include(c => c.CartItems)
                                        .Where(c => c.UserId == userId)
                                        .Select(c => new CartDTO
                                        {
                                            CartId = c.CartId,
                                            UserId = c.UserId,
                                            CartItems = c.CartItems.Select(ci => new CartItemDTO
                                            {
                                                CartItemId = ci.CartItemId,
                                                CartId = ci.CartId,
                                                MovieId = ci.MovieId,
                                                Quantity = ci.Quantity
                                            }).ToList()
                                        })
                                        .FirstOrDefault();

            // If the user doesn't have a cart, create a new one
            if (cartDTO == null)
            {
                cartDTO = new CartDTO
                {
                    UserId = userId,
                    CartItems = new List<CartItemDTO>()
                };
            }

            // Check if the movie is already in the cart
            var cartItemDTO = cartDTO.CartItems.FirstOrDefault(ci => ci.MovieId == movieId);

            if (cartItemDTO != null)
            {
                // Update the quantity if the movie is already in the cart
                cartItemDTO.Quantity += quantity;
            }
            else
            {
                // Create a new cart item if the movie is not in the cart
                cartItemDTO = new CartItemDTO
                {
                    CartId = cartDTO.CartId,
                    MovieId = movieId,
                    Quantity = quantity
                };
                cartDTO.CartItems.Add(cartItemDTO);
            }

            // Convert the DTOs back to entities and save the changes to the database
            var cart = new Cart
            {
                CartId = cartDTO.CartId,
                UserId = cartDTO.UserId,
                CartItems = cartDTO.CartItems.Select(ci => new CartItem
                {
                    CartItemId = ci.CartItemId,
                    CartId = ci.CartId,
                    MovieId = ci.MovieId,
                    Quantity = ci.Quantity
                }).ToList()
            };

            // If the user doesn't have a cart, add the new cart to the database
            if (_context.Carts.FirstOrDefault(c => c.UserId == userId) == null)
            {
                _context.Carts.Add(cart);
            }

            // Save the changes to the database
            _context.SaveChanges();
        }

















    }
}
