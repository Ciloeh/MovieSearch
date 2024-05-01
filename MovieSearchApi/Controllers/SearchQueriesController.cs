using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MovieSearchApi.Services;

/*******************
 * 
 * Written by Jude Iloelunachi
 * 
 */
namespace MovieSearchApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
	// This class is a controller for managing search query-related operations.
	public class SearchQueriesController : ControllerBase
	{
		// A private readonly field for the search query service.
		private readonly ISearchQueryService _searchQueryService;

		// The constructor for the SearchQueriesController class.
		// It takes an ISearchQueryService as a parameter and assigns it to the _searchQueryService field.
		public SearchQueriesController(ISearchQueryService searchQueryService)
		{
			_searchQueryService = searchQueryService;
		}

		// This is an HTTP POST method that saves a search query.
		// It takes a string query as a parameter from the request body.
		// It returns a 200 status code after successfully saving the query.
		[HttpPost]
		public async Task<IActionResult> SaveSearchQuery([FromBody] string query)
		{
			await _searchQueryService.SaveSearchQueryAsync(query);
			return Ok();
		}

		// This is an HTTP GET method that retrieves the latest search queries.
		// It returns a 200 status code with the list of latest search queries.
		[HttpGet]
		public async Task<IActionResult> GetLatestSearchQueries()
		{
			var queries = await _searchQueryService.GetLatestSearchQueriesAsync();
			return Ok(queries);
		}
	}

}
