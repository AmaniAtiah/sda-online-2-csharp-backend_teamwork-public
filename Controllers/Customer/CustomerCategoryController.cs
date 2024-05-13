using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Backend.Dtos;
using System.Security.Claims;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/customers/categories")]
    // customer can show all categories, show categories by id  
    //customer can not update or delete or add categories
    public class CustomerCategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;
        public CustomerCategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [Authorize(Roles = "User")]
        [HttpGet]//Works 
        public async Task<IActionResult> GetAllCategories([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 3)
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
                var categories = await _categoryService.GetAllCategoryAsync(pageNumber, pageSize);
                return ApiResponse.Success(categories, "all categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpGet("{categoryId:guid}")]//Works 
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            try
            {
                var isUser = User.Claims.Any(c => c.Type == ClaimTypes.Role && c.Value == "User");
                if (!isUser)
                {
                    return ApiResponse.Forbidden("Only User can visit this route");
                }
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
    }
}

//     [HttpPost]
//     public async Task<IActionResult> AddCategory([FromBody] CategoryDto newCategory)
//     {
//         if (!ModelState.IsValid)
//         {
//             throw new ValidationException("Invalid  Data");
//         }
//         var category = await _categoryService.AddCategoryAsync(newCategory);
//         return ApiResponse.Created(category, "New Category is added successfully");
//     }

//     [HttpPut("{categoryId:guid}")]
//     public async Task<IActionResult> UpdateCategory(Guid categoryId, CategoryDto updateCategory)
//     {
//         try
//         {
//             var category = await _categoryService.UpdateCategoryAsync(categoryId, updateCategory);
//             if (category == null)
//             {
//                 return ApiResponse.NotFound("Category was not found");
//             }
//             else
//             {
//                 return ApiResponse.Success(category, "Update Category successfully");
//             }
//         }
//         catch (Exception ex)
//         {
//             return ApiResponse.ServerError(ex.Message);
//         }
//     }

//     [HttpDelete("{categoryId:guid}")]
//     public async Task<IActionResult> DeleteCategory(Guid categoryId)
//     {
//         try
//         {
//             var result = await _categoryService.DeleteCategoryAsync(categoryId);
//             if (result)
//             {
//                 return NoContent();
//             }
//             else
//             {
//                 return ApiResponse.NotFound("Category was not found");
//             }
//         }
//         catch (Exception ex)
//         {
//             return ApiResponse.ServerError(ex.Message);
//         }
//     }
// }

