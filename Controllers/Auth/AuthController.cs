using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Dtos;
using Backend.EntityFramework;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Backend.Middlewares;

namespace Backend.Auth
{
   [ApiController]//api controllers
    [Route("/api/auth")]
    // add function register and login for user authentication
    public class AuthController : ControllerBase
    {

        private readonly UserService _userService;
        private readonly AuthService _authService;

        public AuthController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        // // register 
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid User Data");
            }
            var newUser = await _userService.AddUserAsync(registerDto);
            return ApiResponse.Created(newUser, "User created successfully");
            } catch(Exception e){
                return ApiResponse.ServerError(e.Message);
            }
        }

// [HttpPost]
//  [Route("/api/auth/register")]
//   public async Task<IActionResult> Register(RegisterDto registerDto)
//     {
//         try
//         {
//             var userDto = await _userService.AddUserAsync(registerDto);
//             return CreatedAtAction(nameof(Register), userDto);
//         }
//         catch (DuplicateEmailException ex)
//         {
//             return Conflict(new { Message = ex.Message });
//         }
//         catch (DuplicatePhoneNumberException ex)
//         {
//             return Conflict(new { Message = ex.Message });
//         }
//         catch (Exception ex)
//         {
//             // Log exception details here as needed
//             return StatusCode(500, new { Message = "An error occurred while processing your request." });
//         }
//     }


        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> LoginUser([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return ApiResponse.BadRequest("Invalid user data");
            }
            var loggedInUser = await _userService.LoginUserAsync(loginDto);

            if (loggedInUser == null)
            {
                return ApiResponse.BadRequest("Logged-in user is not valid");
            }


            var token = _authService.GenerateJwt(loggedInUser);
            return ApiResponse.Success(new { token, loggedInUser }, "User is logged in successfully");
        }

    }
}