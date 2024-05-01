using MovieSearchApi.Model;

/*******************
 * 
 * Written by Jude Iloelunachi
 * 
 */
namespace MovieSearchApi.Services
{
	// This interface defines the contract for a movie service.
	public interface IMovieService
	{
		// This method retrieves the details of a movie by its title.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a MoviesDetails object.
		Task<MoviesDeatils> GetMovieByTitleAsync(string title);

		// This method searches for movies by their title.
		// It returns a Task that represents the asynchronous operation.
		// The result of the Task is a collection of MoviesDetails objects.
		Task<IEnumerable<MoviesDeatils>> SearchMoviesByTitleAsync(string title);
	}

}
