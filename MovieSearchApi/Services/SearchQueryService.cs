using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieSearchApi.Data;
using MovieSearchApi.Model;
using Newtonsoft.Json;
using System.Net.Http;

namespace MovieSearchApi.Services
{
	// This class implements the ISearchQueryService interface and defines methods for search query-related operations.
	public class SearchQueryService : ISearchQueryService
	{
		// A private readonly field for the database context.
		private readonly MovieDbContext _dbContext;
		// A private readonly field for the HTTP client.
		private readonly HttpClient _httpClient;
		// A private readonly field for the OMDB API settings.
		private readonly OmdbApiSettings _omdbApiSettings;

		// The constructor for the MovieServices class.
		// It takes a MovieDbContext, HttpClient, and IOptions<OmdbApiSettings> as parameters and assigns them to the respective fields.
		public SearchQueryService(MovieDbContext dbContext, HttpClient httpClient, IOptions<OmdbApiSettings> omdbApiSettings)
		{
			_dbContext = dbContext;
			_httpClient = httpClient;
			_omdbApiSettings = omdbApiSettings.Value;
		}
		
		// This method retrieves the latest search queries from the database.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a collection of SearchQuery objects.
		public async Task<IEnumerable<SearchQuery>> GetLatestSearchQueriesAsync(int id)
		{
			return await _dbContext.SearchQueries.Where(o => o.UserId == id)
				.OrderByDescending(sq => sq.TimeStamp) // Orders the search queries by their timestamp in descending order.
				.Take(5) // Takes the top 5 latest search queries.
				.ToListAsync(); // Converts the result to a list.
		}

		// This method saves a search query to the database.
		// It takes a string query as a parameter.
		// It returns a Task that represents the asynchronous operation.
		/****
		public async Task SaveSearchQueryAsync(string query)
		{
			var searchQuery = new SearchQuery { Query = query, TimeStamp = DateTime.UtcNow }; // Creates a new SearchQuery object.
			_dbContext.SearchQueries.Add(searchQuery); // Adds the new SearchQuery object to the database context.
			await _dbContext.SaveChangesAsync(); // Saves the changes to the database.
		}
		*****/
		public async Task SaveSearchQueryAsync(string query, int userid)
		{
			// Fetch the movie details.
			var movieDetails = await GetMovieByTitleAsync(query);

			if (movieDetails != null)
			{
				// Map the movie details to a new SearchQuery object.
				var searchQuery = new SearchQuery
				{
					Title = movieDetails.Title,
					Year = movieDetails.Year,
					Rated = movieDetails.Rated,
					Released = movieDetails.Released,
					Runtime = movieDetails.Runtime,
					Genre = movieDetails.Genre,
					Director = movieDetails.Director,
					Writer = movieDetails.Writer,
					Actors = movieDetails.Actors,
					Plot = movieDetails.Plot,
					Language = movieDetails.Language,
					Country = movieDetails.Country,
					Awards = movieDetails.Awards,
					Poster = movieDetails.Poster,
					Metascore = movieDetails.Metascore,
					ImdbRating = movieDetails.ImdbRating,
					ImdbVotes = movieDetails.ImdbVotes,
					ImdbID = movieDetails.ImdbID,
					Type = movieDetails.Type,
					DVD = movieDetails.DVD,
					BoxOffice = movieDetails.BoxOffice,
					Production = movieDetails.Production,
					UserId = userid,
					Website = movieDetails.Website,
					TimeStamp = DateTime.UtcNow
				};

				// Save the SearchQuery object.
				_dbContext.SearchQueries.Add(searchQuery); // Adds the new SearchQuery object to the database context.
				await _dbContext.SaveChangesAsync();
				
			}
		}

		public async Task<MoviesDeatils> GetMovieByTitleAsync(string title)
		{
			// Construct the API URL.
			var apiUrl = $"{_omdbApiSettings.BaseUrl}?t={title}&apikey={_omdbApiSettings.ApiKey}";

			// Send the HTTP GET request.
			var response = await _httpClient.GetAsync(apiUrl);

			if (response.IsSuccessStatusCode)
			{
				// Read the response content as a string.
				var content = await response.Content.ReadAsStringAsync();

				// Deserialize the JSON string into a MoviesDetails object.
				return JsonConvert.DeserializeObject<MoviesDeatils>(content);
			}

			// If the HTTP response indicates an error, return null.
			return null;
		}

		public async Task DeleteSearchQueryAsync(int id)
		{
			// Find the SearchQuery with the given ID.
			var searchQuery = await _dbContext.SearchQueries.FirstOrDefaultAsync(o => o.Id ==id);

			if (searchQuery != null)
			{
				// Remove the SearchQuery from the database context.
				_dbContext.SearchQueries.Remove(searchQuery);

				// Save the changes to the database.
				await _dbContext.SaveChangesAsync();
			}
		}

	}


}
