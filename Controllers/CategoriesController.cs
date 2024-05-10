using Microsoft.AspNetCore.Mvc;
using Backend.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Backend.Helpers;
using Backend.EntityFramework;
using Backend.Models;

namespace Backend.Controllers
{
    [ApiController]
    [Route("/api/categories")]
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

        [HttpGet("{categoryId:guid}")]
        public async Task<IActionResult> GetCategory(Guid categoryId)
        {
            try
            {
                var category = await _categoriesService.GetCategoryById(categoryId);
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



        [HttpPost]
        public async Task<IActionResult> AddCategory(Category newCategory)
        {
            try
            {
                newCategory.Slug = SlugResponse.GenerateSlug(newCategory.category_name);
                var createdCategory = await _categoriesService.AddCategoryService(newCategory);
                return CreatedAtAction(nameof(GetCategory), new { categoryId = createdCategory.CategoryId }, createdCategory);
            }
            catch (Exception ex)
            {
                return ApiResponse.ServerError(ex.Message);
            }
        }

        [HttpPut("{categoryId:guid}")]
        public async Task<IActionResult> UpdateCategory(Guid categoryId, Category updateCategory)
        {
            try
            {
                var category = await _categoriesService.UpdateCategoryService(categoryId, updateCategory);
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

        [HttpDelete("{categoryId:guid}")]
        public async Task<IActionResult> DeleteCategory(Guid categoryId)
        {
            try
            {
                var result = await _categoriesService.DeleteCategoryService(categoryId);
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