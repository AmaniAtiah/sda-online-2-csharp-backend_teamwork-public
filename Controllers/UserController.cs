
using Backend.Dtos;
using Backend.Dtos.User;
using Backend.Services;
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

   

        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);
            return ApiResponse.Success(users, "All users are returned successfully");

        }

        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            
                var user = await _userService.GetUserByIdAsync(userId);
                if(user == null){
                    return ApiResponse.NotFound("user not found");
                }
                return ApiResponse.Success(user, " user is returned successfully");
           
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDto newUserData)
        {

            if (!ModelState.IsValid)
            {
                return ApiResponse.BadRequest("Invalid user data");
            }
            if (newUserData == null)
            {
                return ApiResponse.BadRequest("Invalid user data");
            }
            var newUser = _userService.CreateUserAsync(newUserData);
            return ApiResponse.Created(newUser, "User is created successfully");

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
            Console.WriteLine($"{token}");


            return ApiResponse.Success(new { token, loggedInUser }, "User is logged in successfully");


        }


    }
}