using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Backend.Controllers;
using Backend.Dtos;
using Backend.Dtos.User;
using Backend.EntityFramework;
using Backend.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Auth
{

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

        // register 
        [HttpPost]
        [Route("/api/auth/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                throw new ValidationException("Invalid User Data");
            }
            var newUser = await _userService.AddUserAsync(registerDto);
            return ApiResponse.Created(newUser, "User created successfully");
        }




        [Route("/api/auth/login")]
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