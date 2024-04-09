using Microsoft.AspNetCore.Mvc;
using Movies.Core.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Movies.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        private readonly ILogger<MovieController> _logger;

        public MovieController(IMovieService movieService, ILogger<MovieController> logger)
        {
            _movieService = movieService;
            _logger = logger;
            _logger.LogDebug(1, "NLog injected into HomeController");
        }


        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMovies();
            _logger.LogInformation("Hello this is logging");
            return Ok(movies);
        }


    }
}
