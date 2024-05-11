
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
    [ApiController]
    [Route("/api/users")]
    public class UserController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly AuthService _authService;


        public UserController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }



        [Authorize(Roles = "Admin")]
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

        [Authorize]
        [HttpGet("profile")]
        public async Task<IActionResult> GetUserById(Guid userId)
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

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUserData)
        {

            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid User Data");
            }


            var newUser = await _userService.CreateUserAsync(newUserData);
            return ApiResponse.Created(newUser, "User created successfully");

        }

        [Authorize]
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

        [HttpPost("login")]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {

            if (!ModelState.IsValid)
            {
                return ApiResponse.BadRequest("Invalid user data");
            }
            var loggedInUser = await _userService.LoginUserAsync(loginDto);
            var token = _authService.GenerateJwt(loggedInUser);



            return ApiResponse.Success(new { token, loggedInUser }, "User is logged in successfully");


        }





    }
}