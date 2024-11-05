using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Movie.Api;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;
using Movies.Repository.Entities;

namespace Movies.Core.Services
{
    public class CartService(IMapper mapper, MovieDbContext context, ILogger<CartService> logger) : ICartService
    {
        private readonly IMapper _mapper = mapper;
        private readonly MovieDbContext _context = context;
        private readonly ILogger<CartService> _logger = logger;

        public async Task<bool> AddMovieToCart(int movieId, int userId, int quantity = 1)
        {
            try
            {
                //Check if the movie exists in the database
                var movie = await _context.Movies.SingleOrDefaultAsync(m => m.Id == movieId);
                if (movie == null)
                {
                    _logger.LogWarning("Movie with ID {MovieId} not found.", movieId);
                    return false; // Movie not found
                }

                //Check if the user has a cart. If not, create one.
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    cart = new Cart
                    {
                        UserId = userId,
                        CartItems = new List<CartItem>()
                    };
                    _context.Carts.Add(cart);
                }

                //Check if the movie is already in the user's cart
                var existingCartItem = cart.CartItems.SingleOrDefault(ci => ci.MovieId == movieId);
                if (existingCartItem != null)
                {
                    // If movie is already in the cart, just update the quantity
                    existingCartItem.Quantity += quantity;
                }
                else
                {
                    // If movie is not in the cart, add a new cart item
                    var cartItem = new CartItem
                    {
                        MovieId = movieId,
                        CartId = cart.CartId,
                        Quantity = quantity,
                        Movie = movie
                    };
                    cart.CartItems.Add(cartItem);
                }

                //Save the changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Movie {MovieId} added to user {UserId}'s cart.", movieId, userId);
                return true; // Movie added successfully
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding movie {MovieId} to user {UserId}'s cart.", movieId, userId);
                return false; // Operation failed
            }
        }


        public async Task<bool> PurchaseCartItems(int userId)
        {
            try
            {
                //Get the user's cart along with cart items
                var cart = await _context.Carts
                    .Include(c => c.CartItems)
                    .ThenInclude(ci => ci.Movie)
                    .SingleOrDefaultAsync(c => c.UserId == userId);

                if (cart == null || !cart.CartItems.Any())
                {
                    _logger.LogWarning("Cart is empty or does not exist for user {UserId}.", userId);
                    return false;
                }

                //Create a new order for the user
                var order = new Order
                {
                    UserId = userId,
                    OrderDate = DateTime.UtcNow,
                    OrderItems = new List<OrderItem>()
                };

                //Transfer items from cart to order
                foreach (var cartItem in cart.CartItems)
                {
                    var orderItem = new OrderItem
                    {
                        MovieId = cartItem.MovieId,
                        Quantity = cartItem.Quantity,
                        Movie = cartItem.Movie
                    };

                    order.OrderItems.Add(orderItem);
                }

                //Add the order to the context
                _context.Orders.Add(order);

                //Clear the user's cart (optional: can also delete the cart or just clear items)
                _context.CartItems.RemoveRange(cart.CartItems);
                // _context.Carts.Remove(cart); // Optionally remove the cart itself

                //Save changes to the database
                await _context.SaveChangesAsync();

                _logger.LogInformation("Order placed successfully for user {UserId}.", userId);
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while placing order for user {UserId}.", userId);
                return false;
            }
        }


        // :TODO Needs testing
        public async Task<bool> RemoveItemFromCart(int id)
        {
            try
            {
                var cart = await _context.CartItems.FindAsync(id);
                if (cart != null)
                {
                    _context.CartItems.Remove(cart);
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

        public async Task<List<MovieDto>> DisplayCartItems(int userId)
        {
            try
            {
                //Retrieve the cart for the specified user
                var cart = await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

                if (cart == null)
                {
                    _logger.LogWarning($"No cart found for user with ID {userId}.");
                    return new List<MovieDto>(); 
                }

                //Retrieve the cart items for this cart ID
                var cartItems = await _context.CartItems
                    .Where(ci => ci.CartId == cart.CartId)
                    .ToListAsync();

                if (cartItems.Count == 0)
                {
                    _logger.LogInformation($"Cart for user ID {userId} is empty.");
                    return new List<MovieDto>();
                }

                //Fetch movies and map them to MovieDto
                var movieIds = cartItems.Select(ci => ci.MovieId).Distinct().ToList();
                var movies = await _context.Movies
                    .Where(m => movieIds.Contains(m.Id))
                    .ToListAsync();

                // Map movies to DTOs, including quantity from cart items
                var movieDtos = movies.Select(movie =>
                {
                    var cartItem = cartItems.First(ci => ci.MovieId == movie.Id);
                    var movieDto = _mapper.Map<MovieDto>(movie);
                    movieDto.Quantity = cartItem.Quantity; 
                    return movieDto;
                }).ToList();

                _logger.LogInformation($"Retrieved {movieDtos} movies successfully for user ID {userId}.");
                return movieDtos;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while retrieving the cart items for user ID {userId}.");
                throw;
            }
        }



    }
}
