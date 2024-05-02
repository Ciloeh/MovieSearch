using Microsoft.EntityFrameworkCore;
using MovieSearchApi.Data;
using MovieSearchApi.Model;

namespace MovieSearchApi.Services
{
	public class UsersService : IUsersService
	{
		private readonly MovieDbContext _dbContext;
		public UsersService(MovieDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<Users> GetUserByIdAsync(string id) => await _dbContext.User.FirstOrDefaultAsync(o => o.phone ==id);

		public async Task<IEnumerable<Users>> GetAllUsersAsync()
		{
			return await _dbContext.User.ToListAsync();
		}

		public async Task<Users> CreateUserAsync(Users user)
		{
			user.dateadded = DateTime.UtcNow;
			_dbContext.User.Add(user);
			await _dbContext.SaveChangesAsync();
			return user;
		}

		public async Task<Users> UpdateUserAsync(Users user)
		{
			_dbContext.User.Update(user);
			await _dbContext.SaveChangesAsync();
			return user;
		}

		public async Task DeleteUserAsync(int id)
		{
			var user = await _dbContext.User.FirstOrDefaultAsync(u => u.Id == id);
			if (user != null)
			{
				_dbContext.User.Remove(user);
				await _dbContext.SaveChangesAsync();
			}
		}

		
	}

}
