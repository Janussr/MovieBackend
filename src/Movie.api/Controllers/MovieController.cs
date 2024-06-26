using Microsoft.AspNetCore.Mvc;
using Movies.Core.Dto;
using Movies.Core.Services.Interfaces;
using Movies.Repository.Entities;

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
            _logger.LogDebug("NLog injected into HomeController");
            _logger.LogInformation("NLog Successful request");

        }

        [HttpPost]
        public async Task<ActionResult<CreateMovieDto>> CreateMovie(CreateMovieDto movie)
        {
            var result = await _movieService.CreateMovie(movie);
            _logger.LogInformation($"Created movie: {result}");
            return Ok(result);
        }


        [HttpGet("/api/Movie/all")]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await _movieService.GetMovies();
            _logger.LogInformation($"{nameof(GetMovies)}");
            return Ok(movies);

        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovieById(int id)
        {
            var result = await _movieService.GetMovieById(id);
            _logger.LogInformation($"Movie {id} {result}");
            return Ok(result);
        }



    }

}
