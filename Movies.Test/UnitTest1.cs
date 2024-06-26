using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Movie.Api;
using Movies.Api.Controllers;
using Movies.Core.Dto;
using Movies.Core.Services;
using Movies.Core.Services.Interfaces;
using Movies.Repository.Entities;

namespace Movies.Test
{
    public class MovieServiceTests 
    {
        private readonly Mock<IMapper> _mockMapper;
        private readonly Mock<ILogger<MovieService>> _mockLogger;
        private readonly MovieDbContext _context;
        private readonly MovieService _movieService;
        private readonly MovieController _movieController;

        public MovieServiceTests()
        {
            _mockMapper = new Mock<IMapper>();
            //_mockLogger = new Mock<ILogger<MovieService>>();

            var logger = Mock.Of<ILogger<MovieController>>();

           // _movieService = Mock.Of<IMovieService>();
            _movieController = new MovieController(_movieService, logger);
        }


        [Fact]
        public async Task GivenCorrectId_WhenGettingById_ThenReturnFolder()
        {
            // Arrange
            const int id = 1;
            var movie = new MovieDto
            {
                Id = id,
                Title = "Avatar",
            };
            Mock.Get(_movieService).Setup(s => s.GetMovieById(id)).ReturnsAsync(movie);

            // Act
            var result = await _movieController.GetMovieById(id);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(movie, okResult.Value);
        }


       

        public void Dispose()
        {
           // _context.Database.EnsureDeleted(); // Clean up after each test
            //_context.Dispose();
        }
    }
}
