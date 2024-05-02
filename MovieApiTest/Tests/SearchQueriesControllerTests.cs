using Microsoft.AspNetCore.Mvc;
using MovieSearchApi.Controllers;
using MovieSearchApi.Services;
using Xunit;
using Moq;
using MovieSearchApi.Model;

namespace MovieSearchApi.Tests
{
    public class SearchQueriesControllerTests
    {
        private readonly Mock<ISearchQueryService> _mockSearchQueryService;
        private readonly SearchQueriesController _controller;

        public SearchQueriesControllerTests()
        {
            _mockSearchQueryService = new Mock<ISearchQueryService>();
            _controller = new SearchQueriesController(_mockSearchQueryService.Object);
        }

        [Fact]
        public async Task SaveSearchQuery_ReturnsOk_WhenCalled()
        {
            // Arrange
            var query = "test query";

            // Act
            var result = await _controller.SaveSearchedQuery(query);

            // Assert
            Assert.IsType<OkResult>(result);
            _mockSearchQueryService.Verify(service => service.SaveSearchQueryAsync(query), Times.Once);
        }

        [Fact]
		public async Task GetLatestSearchQueries_ReturnsOkWithQueries_WhenCalled()
		{
			// Arrange
			var expectedQueries = new List<SearchQuery>
	{
		new SearchQuery
		{
			Id = 1,
			Title = "title1",
			Year = "year1",
			Rated = "rated1",
			Released = "released1",
			Runtime = "runtime1",
			Genre = "genre1",
			Director = "director1",
			Writer = "writer1",
			Actors = "actors1",
			Plot = "plot1",
			Language = "language1",
			Country = "country1",
			Awards = "awards1",
			Poster = "poster1",
			Metascore = "metascore1",
			ImdbRating = "imdbRating1",
			ImdbVotes = "imdbVotes1",
			ImdbID = "imdbID1",
			Type = "type1",
			DVD = "dvd1",
			BoxOffice = "boxOffice1",
			Production = "production1",
			Website = "website1",
			TimeStamp = DateTime.UtcNow
		},
        //  More SearchQuery can be Added here
    };
			_mockSearchQueryService.Setup(service => service.GetLatestSearchQueriesAsync())
				.ReturnsAsync(expectedQueries);

			// Act
			var actionResult = await _controller.GetLatestSearchQueries();

			// Assert
			var okResult = actionResult as OkObjectResult;
			Assert.NotNull(okResult);
			var returnedQueries = okResult.Value as IEnumerable<SearchQuery>;
			Assert.NotNull(returnedQueries);
			Assert.Equal(expectedQueries, returnedQueries);
		}


		[Fact]
		public async Task DeleteSearchQuery_ReturnsOk_WhenCalledWithValidId()
		{
			// Arrange
			int testId = 1; 

			// Set up the mock service to complete successfully when DeleteSearchQueryAsync is called.
			_mockSearchQueryService.Setup(service => service.DeleteSearchQueryAsync(testId))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.DeleteSearchedQuery(testId);

			// Assert
			var okResult = Assert.IsType<OkResult>(result);
			Assert.Equal(200, okResult.StatusCode);
		}

	}


}
