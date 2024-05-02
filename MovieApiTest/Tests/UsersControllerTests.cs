using Microsoft.AspNetCore.Mvc;
using Moq;
using MovieSearchApi.Controllers;
using MovieSearchApi.Model;
using MovieSearchApi.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieApiTest.Tests
{
	public class UsersControllerTests
	{
		private readonly Mock<IUsersService> _mockUsersService;
		private readonly UsersController _controller;

		public UsersControllerTests()
		{
			_mockUsersService = new Mock<IUsersService>();
			_controller = new UsersController(_mockUsersService.Object);
		}

		[Fact]
		public async Task GetUserById_ReturnsUser_WhenUserExists()
		{
			// Arrange
			var expectedUser = new Users { Id = 1, Name = "Test User" };
			_mockUsersService.Setup(service => service.GetUserByIdAsync(expectedUser.Id))
				.ReturnsAsync(expectedUser);

			// Act
			var result = await _controller.GetUserById(expectedUser.Id);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedUser = Assert.IsType<Users>(okResult.Value);
			Assert.Equal(expectedUser, returnedUser);
		}

		[Fact]
		public async Task GetAllUsers_ReturnsAllUsers()
		{
			// Arrange
			var expectedUsers = new List<Users> { new Users { Id = 1, Name = "Test User 1" }, new Users { Id = 2, Name = "Test User 2" } };
			_mockUsersService.Setup(service => service.GetAllUsersAsync())
				.ReturnsAsync(expectedUsers);

			// Act
			var result = await _controller.GetAllUsers();

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedUsers = Assert.IsType<List<Users>>(okResult.Value);
			Assert.Equal(expectedUsers, returnedUsers);
		}

		[Fact]
		public async Task CreateUser_ReturnsCreatedUser()
		{
			// Arrange
			var newUser = new Users { Id = 3, Name = "Test User 3" };
			_mockUsersService.Setup(service => service.CreateUserAsync(newUser))
				.ReturnsAsync(newUser);

			// Act
			var result = await _controller.CreateUser(newUser);

			// Assert
			var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
			var returnedUser = Assert.IsType<Users>(createdAtActionResult.Value);
			Assert.Equal(newUser, returnedUser);
		}

		[Fact]
		public async Task UpdateUser_ReturnsUpdatedUser()
		{
			// Arrange
			var updatedUser = new Users { Id = 1, Name = "Updated User" };
			_mockUsersService.Setup(service => service.UpdateUserAsync(updatedUser))
				.ReturnsAsync(updatedUser);

			// Act
			var result = await _controller.UpdateUser(updatedUser.Id, updatedUser);

			// Assert
			var okResult = Assert.IsType<OkObjectResult>(result);
			var returnedUser = Assert.IsType<Users>(okResult.Value);
			Assert.Equal(updatedUser, returnedUser);
		}

		[Fact]
		public async Task DeleteUser_ReturnsNoContent_WhenUserExists()
		{
			// Arrange
			var userIdToDelete = 1;
			_mockUsersService.Setup(service => service.DeleteUserAsync(userIdToDelete))
				.Returns(Task.CompletedTask);

			// Act
			var result = await _controller.DeleteUser(userIdToDelete);

			// Assert
			Assert.IsType<NoContentResult>(result);
		}
	}

}
