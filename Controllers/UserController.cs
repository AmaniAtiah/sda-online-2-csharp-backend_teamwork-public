using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using Backend.Dtos;
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

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            var users = await _userService.GetAllUsersAsync(pageNumber, pageSize);

            // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            // if (!isAdmin)
            // {
            //     return ApiResponse.Forbidden("Only admin can visit this route");
            // }
            return ApiResponse.Success(users, "All users are returned successfully");
        }

        [Authorize]
        [HttpGet("{userId:guid}")]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // if (string.IsNullOrEmpty(userIdString))
            // {
            //     return ApiResponse.UnAuthorized("User Id is misisng from token");
            // }
            // if (!Guid.TryParse(userIdString, out userId))
            // {
            //     return ApiResponse.BadRequest("Invalid User Id");
            // }

            var user = await _userService.GetUserByIdAsync(userId) ?? throw new NotFoundException("User not found");
            return ApiResponse.Success(user, "User profile is returned successfully");
        }



  // banned and unbanned users 
      [Authorize(Roles = "Admin")]
        [HttpPut("ban-unban/{userId:guid}")]
        public async Task<IActionResult> BanAndUnBannedUser(Guid userId)
        {
              var isBanned = await _userService.BannedAndUnbannedUserAsync(userId);
        if (isBanned)
        {
            var user = await _userService.GetUserByIdAsync(userId);
            var message = user.IsBanned ? $"User with this id  {userId} has been banned" : $"User with this id  {userId} has been unbanned";
            return ApiResponse.Success(message);
        }
        else
        {
            return ApiResponse.NotFound("User not found.");
        }



            
       
        }








        // [HttpPost]
        // public async Task<IActionResult> CreateUser([FromBody] RegisterDto registerDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         throw new ValidationException("Invalid User Data");
        //     }
        //     var newUser = await _userService.AddUserAsync(registerDto);
        //     return ApiResponse.Created(newUser, "User created successfully");
        // }


        [Authorize]
        [HttpPut("{userId:guid}")]
        // admin profile
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UpdateUserDto updateUserDto)
        {
            // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // if (string.IsNullOrEmpty(userIdString))
            // {
            //     return ApiResponse.UnAuthorized("User Id is misisng from token");
            // }

            // if (!Guid.TryParse(userIdString, out userId))
            // {
            //     return ApiResponse.BadRequest("Invalid User Id");
            // }

            if (!ModelState.IsValid)
            {
                return ApiResponse.BadRequest("Invalid User data");
            }

            var updateUser = await _userService.UpdateUserAsync(userId, updateUserDto) ?? throw new NotFoundException("User not found");
            if(updateUser == null) {
                return ApiResponse.NotFound("User was not found");
            }
            return ApiResponse.Success(updateUser, "User updated successfully");
        }



         [Authorize]
        [HttpDelete("{userId:guid}")]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            // var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            // if (string.IsNullOrEmpty(userIdString))
            // {
            //     return ApiResponse.UnAuthorized("User Id is misisng from token");
            // }
            // if (!Guid.TryParse(userIdString, out userId))
            // {
            //     return ApiResponse.BadRequest("Invalid User Id");
            // }
                   if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid user data provided");
            }
            await _userService.DeleteUserAsync(userId);
            return NoContent();
        }



    }
}