using Microsoft.AspNetCore.Mvc;
using Movies.Core.Services;
using Movies.Core.Services.Interfaces;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;


        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
            _logger.LogDebug("NLog injected into HomeController");
            _logger.LogInformation("NLog Successful request");

        }



        [HttpPost("/api/cart/AddMovieToCart")]
        public async Task<IActionResult> AddMovieToCart(int movieId, int userId, int quantity = 1)
        {
            var result = await _cartService.AddMovieToCart(movieId, userId, quantity);
            if (!result)
            {
                return BadRequest();
            }
            return Ok("movie added to cart");
        }

        [HttpPost("/api/cart/PurchaseCartItems")]
        public async Task<IActionResult> PurchaseCartItems(int userId)
        {
            try
            {
                var result = await _cartService.PurchaseCartItems(userId);

                if (!result)
                {
                    return BadRequest(new
                    {
                        Message = "Failed to place the order. Cart might be empty or there was an issue with the process."
                    });
                }

                return Ok(new
                {
                    Message = "Order successfully placed.",
                    UserId = userId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while placing the order for user {UserId}.", userId);
                return StatusCode(500, new
                {
                    Message = "An internal server error occurred while placing the order."
                });
            }
        }

        [HttpGet("/api/cart/Items")]
        public async Task<IActionResult> DisplayCartItems(int userId)
        {
            try
            {
                var cartItems = await _cartService.DisplayCartItems(userId);

                if (cartItems == null || !cartItems.Any())
                {
                    _logger.LogInformation("No items found in cart for user {UserId}.", userId);
                    return NotFound(new { Message = "Cart is empty or user not found." });
                }

                _logger.LogInformation("Retrieved {ItemCount} items in cart for user {UserId}.", cartItems.Count, userId);
                return Ok(cartItems); // Returns the list of MovieDto items
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the cart items for user {UserId}.", userId);
                return StatusCode(500, new { Message = "An internal server error occurred while retrieving the cart items." });
            }
        }

    }
}
