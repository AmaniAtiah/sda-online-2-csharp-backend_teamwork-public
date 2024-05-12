using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Backend.Dtos;
using Backend.Dtos.User;
using Backend.Middlewares;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.Controllers
{

    [Authorize(Roles = "Admin")]
    [ApiController]
    [Route("/api/admins")]
    public class AdminController : ControllerBase
    {
        private readonly UserService _userService;

        public AdminController(UserService userService)
        {
            _userService = userService;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            if (!isAdmin)
            {
                return ApiResponse.Forbidden("Only admin can visit this route");
            }
            return ApiResponse.Success(users, "All users are returned successfully");
        }


        [HttpGet("profile")]
        // display profile admin
        public async Task<IActionResult> GetAdminProfile(Guid userId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is misisng from token");
            }
            if (!Guid.TryParse(userIdString, out userId))
            {
                return ApiResponse.BadRequest("Invalid User Id");
            }

            var user = await _userService.GetUserByIdAsync(userId) ?? throw new NotFoundException("User not found");
            return ApiResponse.Success(user, "User profile is returned successfully");
        }


        [HttpGet("users/{userId}")]

        // admin can show any details user
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            if (!isAdmin)
            {
                return ApiResponse.Forbidden("Only admin can visit this route");
            }

            var user = await _userService.GetUserByIdAsync(userId) ?? throw new NotFoundException("User not found");
            return ApiResponse.Success(user, "User profile is returned successfully");
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid User Data");
            }
            var newUser = await _userService.AddUserAsync(registerDto);
            return ApiResponse.Created(newUser, "User created successfully");
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateUser(Guid userId, UpdateUserDto updateUserDto)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is misisng from token");
            }

            if (!Guid.TryParse(userIdString, out userId))
            {
                return ApiResponse.BadRequest("Invalid User Id");
            }

            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid User Data");
            }

            var updateUser = await _userService.UpdateUserAsync(userId, updateUserDto) ?? throw new NotFoundException("User not found");
            return ApiResponse.Success(updateUser, "User updated successfully");
        }



        [Authorize]
        [HttpDelete("profile")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString))
            {
                return ApiResponse.UnAuthorized("User Id is misisng from token");
            }
            if (!Guid.TryParse(userIdString, out userId))
            {
                return ApiResponse.BadRequest("Invalid User Id");
            }
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }



    }
}