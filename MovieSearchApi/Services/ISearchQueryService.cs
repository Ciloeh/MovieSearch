﻿using MovieSearchApi.Model;

namespace MovieSearchApi.Services
{
	// This interface defines the contract for a search query service.
	public interface ISearchQueryService
	{
		// This method saves a search query.
		// It takes a string query as a parameter.
		// It returns a Task that represents the asynchronous operation.
		Task SaveSearchQueryAsync(string query);

		// This method retrieves the latest search queries.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a collection of SearchQuery objects.
		Task<IEnumerable<SearchQuery>> GetLatestSearchQueriesAsync();
	}

}
