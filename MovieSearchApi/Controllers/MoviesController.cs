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
	// This class is a controller for managing movie-related operations.
	public class MoviesController : ControllerBase
	{
		// A private readonly field for the movie service.
		private readonly IMovieService _movieService;

		// The constructor for the MoviesController class.
		// It takes an IMovieService as a parameter and assigns it to the _movieService field.
		public MoviesController(IMovieService movieService)
		{
			_movieService = movieService;
		}

		// This is an HTTP GET method that retrieves a movie by its title.
		// It returns a 404 status code if the movie is not found, 
		// and a 200 status code with the movie data if it is found.
		[HttpGet("{title}")]
		public async Task<IActionResult> GetMovieByTitle(string title)
		{
			var movie = await _movieService.GetMovieByTitleAsync(title);
			if (movie == null)
			{
				return NotFound();
			}
			return Ok(movie);
		}

		// This is an HTTP GET method that searches for movies by their title.
		// It returns a 200 status code with the list of movies that match the title.
		[HttpGet("search/{title}")]
		public async Task<IActionResult> SearchMoviesByTitle(string title)
		{
			var movies = await _movieService.SearchMoviesByTitleAsync(title);
			return Ok(movies);
		}
	}

}
