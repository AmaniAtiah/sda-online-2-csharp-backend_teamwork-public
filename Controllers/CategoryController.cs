using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace Backend.Controllers
{
  
    [ApiController]
    [Route("/api/categories")]
    // admin can show all categories and add or delete 


    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }
                var categories = await _categoryService.GetAllCategoryAsync(pageNumber, pageSize);
                return ApiResponse.Success(categories, "all categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }
                var category = await _categoryService.GetCategoryByIdAsync(categoryId);
                if (category != null)
                {
                    return ApiResponse.Success(category, "Category is retrieved successfully");
                }
                else
                {
                    return ApiResponse.NotFound("Category was not found");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryDto newCategory)
        {
            // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
            // if (!isAdmin)
            // {
            //     return ApiResponse.Forbidden("Only admin can visit this route");
            // }
            if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid category data provided");
            }
            var category = await _categoryService.AddCategoryAsync(newCategory);
            return ApiResponse.Created(category, "New Category is added successfully");
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, CategoryDto updateCategory)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }
             if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid category data provided");
            }
                var category = await _categoryService.UpdateCategoryAsync(categoryId, updateCategory);
                if (category == null)
                {
                    return ApiResponse.NotFound("Category was not found");
                }
                else
                {
                    return ApiResponse.Success(category, "Update Category successfully");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            try
            {
                // var isAdmin = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "Admin");
                // if (!isAdmin)
                // {
                //     return ApiResponse.Forbidden("Only admin can visit this route");
                // }
                  if (!ModelState.IsValid)
            {
                ApiResponse.BadRequest("invalid category data provided");
            }
                var result = await _categoryService.DeleteCategoryAsync(categoryId);
                if (result)
                {
                    return NoContent();
                }
                else
                {
                    return ApiResponse.NotFound("Category was not found");
                }
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }
    }
}