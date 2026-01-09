using FitnessTracker.Application.Common;
using FitnessTracker.Application.DTOs.User;
using FitnessTracker.Application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace FitnessTracker.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UsersController> _logger;

        public UsersController(
            IUserService userService,
            ILogger<UsersController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            _logger.LogInformation("GET all users requested");

            var result = await _userService.GetAllAsync();
            if (!result.Success)
            {
                _logger.LogError("Failed to get users: {Error}", result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("Successfully returned {Count} users",
                result.Data?.Count() ?? 0);

            return Ok(result.Data);
        }


        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUser(int id)
        {
            var result = await _userService.GetByIdAsync(id);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.UserNotFound)
                {
                    return NotFound(result.Error);
                }

                return BadRequest(result.Error);
            }

            return Ok(result.Data);
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UpdateUserDto updateUserDto)
        {
            _logger.LogInformation("Updating user id={UserId}", id);

            var result = await _userService.UpdateAsync(id, updateUserDto);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.UserNotFound)
                {
                    _logger.LogWarning("Cannot update user id={UserId} – not found", id);
                    return NotFound(result.Error);
                }

                _logger.LogError("Failed to update user id={UserId}: {Error}", id, result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("User id={UserId} updated successfully", id);
            return Ok(result.Data);
        }


        // POST: api/Users
        [HttpPost]
        public async Task<ActionResult<UserDto>> PostUser(CreateUserDto createUserDto)
        {
            _logger.LogInformation(
                "Creating user with email={Email}",
                createUserDto.Email);

            var result = await _userService.CreateAsync(createUserDto);

            if (!result.Success)
            {
                _logger.LogError("Failed to create user with email={Email}: {Error}",
                    createUserDto.Email, result.Error);

                return BadRequest(result.Error);
            }

            _logger.LogInformation("User created successfully with id={UserId}", result.Data!.Id);

            return CreatedAtAction(
                nameof(GetUser),
                new { id = result.Data.Id },
                result.Data
            );
        }


        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user id={UserId}", id);

            var result = await _userService.DeleteAsync(id);

            if (!result.Success)
            {
                if (result.Error == ErrorMessages.UserNotFound)
                {
                    _logger.LogWarning("Cannot delete user id={UserId} – not found", id);
                    return NotFound(result.Error);
                }

                _logger.LogError("Failed to delete user id={UserId}: {Error}", id, result.Error);
                return BadRequest(result.Error);
            }

            _logger.LogInformation("User id={UserId} deleted successfully", id);
            return NoContent();
        }
    }
}
