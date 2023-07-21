using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieBackend.Services;

namespace MovieBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {


        private readonly MovieService _movieService;

        public MoviesController(MovieService movieService)
        {
            _movieService = movieService;
        }




        [HttpGet]
        [Route("GetMovies")]
        public IActionResult Get()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }

    }
}
