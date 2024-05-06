using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Helpers;
using Backend.EntityFramework;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/Backend/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;
        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var categories = await _categoriesService.GetAllCategoryService();
                return ApiResponse.Success(categories, "all categories retrieved successfully");
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpGet("{category_id:guid}")]
        public async Task<IActionResult> GetCategory(Guid category_id)
        {
            try
            {
                var category = await _categoriesService.GetCategoryById(category_id);
                if (category != null)
                {
                    return ApiResponse.Created(category);
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

        [HttpPost]
        public async Task<IActionResult> CreateCategory(Categories newCategory)
        {
            try
            {
                newCategory.Slug = SlugResponse.GenerateSlug(newCategory.category_name);
                var createdCategory = await _categoriesService.CreateCategoryService(newCategory);
                return CreatedAtAction(nameof(GetCategory), new { id = createdCategory.category_id },createdCategory);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpPut("{category_id:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid category_id, Categories updateCategory)
        {
            try
            {
                var category = await _categoriesService.UpdateCategoryService(category_id, updateCategory);
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

        [HttpDelete("{category_id:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid category_id)
        {
            try
            {
                var result = await _categoriesService.DeleteCategoryService(category_id);
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