using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MovieSearchApi.Model;
using MovieSearchApi.Services;

namespace MovieSearchApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]

	public class UsersController : ControllerBase
	{
		private readonly IUsersService _usersService;

		public UsersController(IUsersService usersService)
		{
			_usersService = usersService;
		}

		// GET api/users/{id}
		[HttpGet("{id}")]
		public async Task<IActionResult> GetUserById(string id)
		{
			var user = await _usersService.GetUserByIdAsync(id);
			if (user == null)
			{
				return NotFound();
			}
			return Ok(user);
		}

		// GET api/users
		[HttpGet]
		public async Task<IActionResult> GetAllUsers()
		{
			var users = await _usersService.GetAllUsersAsync();
			return Ok(users);
		}

		// POST api/users
		[HttpPost]
		public async Task<IActionResult> CreateUser([FromBody] Users user)
		{
			var existingUser = await _usersService.GetUserByIdAsync(user.phone);

			if (existingUser != null)
			{
				// If the user exists, return the user details
				return Ok(existingUser);
			}
			else
			{
				var createdUser = await _usersService.CreateUserAsync(user);
				return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
			}
		}
			

		// PUT api/users/{id}
		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateUser(int id, [FromBody] Users user)
		{
			if (id != user.Id)
			{
				return BadRequest();
			}
			var updatedUser = await _usersService.UpdateUserAsync(user);
			return Ok(updatedUser);
		}

		// DELETE api/users/{id}
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteUser(int id)
		{
			await _usersService.DeleteUserAsync(id);
			return NoContent();
		}
	}

}
