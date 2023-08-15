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
        [Route("GetMoviesByPage")]
        public IActionResult GetMoviesByPage(int pageNumber, int moviesPerPage)
        {
            var movies = _movieService.GetMoviesByPage(pageNumber, moviesPerPage);
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


        [HttpPost]
        [Route("AddToCart")]
        public IActionResult AddToCart(int userId, int movieId, int quantity)
        {
            try
            {
                _movieService.AddToCart(userId, movieId, quantity);
                return Ok("Movie successfully added to cart.");
            }
            catch (System.Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, $"An error occurred: {ex.Message}");
            }
        }






    }
}
