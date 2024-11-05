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


        [HttpGet("/api/Movie/All")]
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


        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateMovie(int id, [FromBody] MovieDto movieDto)
        {
            if (movieDto.Id != id)
            {
                return BadRequest("Movie ID mismatch or movie data is null");
            }

            var result = await _movieService.UpdateMovie(movieDto);
            if (!result)
            {
                return NotFound(); // Return 404 if the movie was not found or update failed
            }
            return NoContent(); // Return 204 No Content if the update was successful
        }



        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await _movieService.DeleteMovie(id);
            if (!result)
            {
                return NotFound(); // Return 404 if the movie was not found or deletion failed
            }
            return Ok("Movie Deleted");
            // return NoContent(); Return 204 No Content if the deletion was successful
        }


        


    }
}
