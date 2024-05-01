using Microsoft.EntityFrameworkCore;
using MovieSearchApi.Data;
using MovieSearchApi.Model;

namespace MovieSearchApi.Services
{
	// This class implements the ISearchQueryService interface and defines methods for search query-related operations.
	public class SearchQueryService : ISearchQueryService
	{
		// A private readonly field for the database context.
		private readonly MovieDbContext _dbContext;

		// The constructor for the SearchQueryService class.
		// It takes a MovieDbContext as a parameter and assigns it to the _dbContext field.
		public SearchQueryService(MovieDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		// This method retrieves the latest search queries from the database.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a collection of SearchQuery objects.
		public async Task<IEnumerable<SearchQuery>> GetLatestSearchQueriesAsync()
		{
			return await _dbContext.SearchQueries
				.OrderByDescending(sq => sq.TimeStamp) // Orders the search queries by their timestamp in descending order.
				.Take(5) // Takes the top 5 latest search queries.
				.ToListAsync(); // Converts the result to a list.
		}

		// This method saves a search query to the database.
		// It takes a string query as a parameter.
		// It returns a Task that represents the asynchronous operation.
		public async Task SaveSearchQueryAsync(string query)
		{
			var searchQuery = new SearchQuery { Query = query, TimeStamp = DateTime.UtcNow }; // Creates a new SearchQuery object.
			_dbContext.SearchQueries.Add(searchQuery); // Adds the new SearchQuery object to the database context.
			await _dbContext.SaveChangesAsync(); // Saves the changes to the database.
		}
	}


}
