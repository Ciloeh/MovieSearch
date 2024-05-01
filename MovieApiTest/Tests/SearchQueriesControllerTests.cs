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
            var result = await _controller.SaveSearchQuery(query);

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
            new SearchQuery { Query = "query1" },
            new SearchQuery { Query = "query2" },
            new SearchQuery { Query = "query3" }
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
    }


}
