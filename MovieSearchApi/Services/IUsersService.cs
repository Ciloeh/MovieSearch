using MovieSearchApi.Model;

namespace MovieSearchApi.Services
{
	public interface IUsersService
	{
		Task<Users> GetUserByIdAsync(string id);
		Task<IEnumerable<Users>> GetAllUsersAsync();
		Task<Users> CreateUserAsync(Users user);
		Task<Users> UpdateUserAsync(Users user);
		Task DeleteUserAsync(int id);
	}

}
