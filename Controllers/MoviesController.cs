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
        public IActionResult GetAllMovies()
        {
            var movies = _movieService.GetAllMovies();
            return Ok(movies);
        }


        [HttpGet]
        [Route("SortMovieByYearASC")]
        public IActionResult SortMovieByYearASC()
        {
            var movies = _movieService.GetAllMoviesOrderedByYearASC();
            return Ok(movies);
        }

        [HttpGet]
        [Route("SortMovieByDESC")]
        public IActionResult SortMovieByYearDESC()
        {
            var movies = _movieService.GetAllMoviesOrderedByYearDESC();
            return Ok(movies);
        }



    }
}
