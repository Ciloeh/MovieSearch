using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MovieSearchApi.Data;
using MovieSearchApi.Model;
using Newtonsoft.Json;


namespace MovieSearchApi.Services
{
	// This class implements the IMovieService interface and defines methods for movie-related operations.
	public class MovieServices : IMovieService
	{
		// A private readonly field for the database context.
		private readonly MovieDbContext _dbContext;
		// A private readonly field for the HTTP client.
		private readonly HttpClient _httpClient;
		// A private readonly field for the OMDB API settings.
		private readonly OmdbApiSettings _omdbApiSettings;

		// The constructor for the MovieServices class.
		// It takes a MovieDbContext, HttpClient, and IOptions<OmdbApiSettings> as parameters and assigns them to the respective fields.
		public MovieServices(MovieDbContext dbContext, HttpClient httpClient, IOptions<OmdbApiSettings> omdbApiSettings)
		{
			_dbContext = dbContext;
			_httpClient = httpClient;
			_omdbApiSettings = omdbApiSettings.Value;
		}

		// This method retrieves the details of a movie by its title from the OMDB API.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a MoviesDetails object.
		public async Task<MoviesDeatils> GetMovieByTitleAsync(string title)
		{
			var response = await _httpClient.GetAsync($"{_omdbApiSettings.BaseUrl}?t={title}&apikey={_omdbApiSettings.ApiKey}");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				return JsonConvert.DeserializeObject<MoviesDeatils>(content);
			}

			return null;
		}

		// This method searches for movies by their title from the OMDB API.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a collection of MoviesDetails objects.
		public async Task<IEnumerable<MoviesDeatils>> SearchMoviesByTitleAsync(string title)
		{
			var response = await _httpClient.GetAsync($"{_omdbApiSettings.BaseUrl}?s={title}&apikey={_omdbApiSettings.ApiKey}");

			if (response.IsSuccessStatusCode)
			{
				var content = await response.Content.ReadAsStringAsync();
				var searchResult = JsonConvert.DeserializeObject<SearchResult>(content);
				return searchResult.Search;
			}

			return null;
		}
	}

}
