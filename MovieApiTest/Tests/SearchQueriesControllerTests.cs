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
			var userid = 1;

			// Act
			var result = await _controller.SaveSearchedQuery(query, userid);

			// Assert
			Assert.IsType<OkResult>(result);
			_mockSearchQueryService.Verify(service => service.SaveSearchQueryAsync(query, userid), Times.Once);
		}

		[Fact]
		public async Task GetLatestSearchQueries_ReturnsOkWithQueries_WhenCalled()
		{
			// Arrange
			var id = 1;
			var expectedQueries = new List<SearchQuery>
			{
				// Add your SearchQuery objects here
			};
			_mockSearchQueryService.Setup(service => service.GetLatestSearchQueriesAsync(id))
				.ReturnsAsync(expectedQueries);

			// Act
			var result = await _controller.GetLatestSearchQueries(id);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedQueries = Assert.IsType<List<SearchQuery>>(okResult.Value);
			Assert.Equal(expectedQueries, returnedQueries);
		}

		[Fact]
		public async Task DeleteSearchQuery_ReturnsOk_WhenCalledWithValidId()
		{
			// Arrange
			int id = 1;

			// Set up the mock service to complete successfully when DeleteSearchQueryAsync is called.
			_mockSearchQueryService.Setup(service => service.DeleteSearchQueryAsync(id))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.DeleteSearchedQuery(id);

			// Assert
			var okResult = Assert.IsType<OkResult>(result);
			Assert.Equal(200, okResult.StatusCode);
		}


	}


}
