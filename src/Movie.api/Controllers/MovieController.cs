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
            _logger.LogDebug( "NLog injected into HomeController");
            _logger.LogInformation( "NLog Successful request");

        }


        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMovies();
            _logger.LogInformation( "log info");
            _logger.LogInformation( "log info");
            return Ok(movies);

        }
    }


}
