using api.Data;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/api/users")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;
    public UserController(AppDbContext appDbContext)
    {
        _userService = new UserService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllUsersAsync();
            if (users.ToList().Count < 1)
            {
                return ApiResponse.NotFound("No users found");
            }
            return ApiResponse.Success(users, "All users are returned");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpGet("{userId:guid}")]
    public async Task<IActionResult> GetUserById(Guid userId)
    {
        try
        {
            var user = await _userService.GetUserAsync(userId);
            if (user == null)
            {
                return ApiResponse.NotFound("User was not found");
            }
            return ApiResponse.Created(user);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);

        }
    }

    [HttpPost]
    public async Task<IActionResult> AddUser(User user)
    {
        try
        {
            var response = await _userService.AddUserAsync(user);
            return ApiResponse.Created(response);
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPut("{userId:guid}")]
    public async Task<IActionResult> UpdateUser(Guid UserId, User user)
    {
        try
        {
            var updateUser = await _userService.UpdateUserAsync(UserId, user);
            if (updateUser == null)
            {
                return ApiResponse.NotFound("User was not found");
            }
            return ApiResponse.Success(updateUser, "User updated successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpDelete("{userId:guid}")]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        try
        {
            var result = await _userService.DeleteUserAsync(userId);
            if (!result)
            {
                return ApiResponse.NotFound("User was not found");
            }
            else
            {
                return NoContent();
            }
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }
}