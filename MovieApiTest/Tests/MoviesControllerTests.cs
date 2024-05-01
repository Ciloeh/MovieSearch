using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieSearchApi.Controllers;
using MovieSearchApi.Model;
using MovieSearchApi.Services;
using Xunit;

/*******************
 * 
 * Written by Jude Iloelunachi
 * 
 */

namespace MovieSearchApi.Tests
{
    public class MoviesControllerTests
    {
        private readonly Mock<IMovieService> _mockMovieService;
        private readonly MoviesController _controller;

        public MoviesControllerTests()
        {
            _mockMovieService = new Mock<IMovieService>();
            _controller = new MoviesController(_mockMovieService.Object);
        }

        [Fact]
        public async Task GetMovieByTitle_ReturnsNotFound_WhenMovieDoesNotExist()
        {
            // Arrange
            _mockMovieService.Setup(service => service.GetMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync((MoviesDeatils)null);

            // Act
            var result = await _controller.GetMovieByTitle("Nonexistent Movie");

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task GetMovieByTitle_ReturnsMovie_WhenMovieExists()
        {
            // Arrange
            var expectedMovie = new MoviesDeatils { Title = "Existing Movie" };
            _mockMovieService.Setup(service => service.GetMovieByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMovie);

            // Act
            var result = await _controller.GetMovieByTitle("Existing Movie");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovie = Assert.IsType<MoviesDeatils>(okResult.Value);
            Assert.Equal(expectedMovie.Title, returnedMovie.Title);
        }

        [Fact]
        public async Task SearchMoviesByTitle_ReturnsMovies_WhenMoviesExist()
        {
            // Arrange
            var expectedMovies = new List<MoviesDeatils>
        {
            new MoviesDeatils { Title = "Movie1" },
            new MoviesDeatils { Title = "Movie2" },
            new MoviesDeatils { Title = "Movie3" }
        };
            _mockMovieService.Setup(service => service.SearchMoviesByTitleAsync(It.IsAny<string>()))
                .ReturnsAsync(expectedMovies);

            // Act
            var result = await _controller.SearchMoviesByTitle("Movie");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedMovies = Assert.IsType<List<MoviesDeatils>>(okResult.Value);
            Assert.Equal(expectedMovies, returnedMovies);
        }
    }

}
