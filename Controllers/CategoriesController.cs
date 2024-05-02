using Microsoft.AspNetCore.Mvc;
using api.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using api.Helpers;

namespace api.Controllers;

[ApiController]
[Route("/api/categories")]
public class CategoriesController : ControllerBase
{

  private readonly CategoriesService _dbContext;
  public CategoriesController(CategoriesService CategoriesService)
  {
    _dbContext = CategoriesService;
  }


  [HttpGet]
  public async Task<IActionResult> GetAllCategories()
  {
    try
    {
      var categories = await _dbContext.GetAllCategoryService();

      if (categories.ToList().Count < 1)
      {
        return NotFound(new ErrorResponse
        {
          Message = "No Categories To Display"
        });
      }
      return Ok(new SuccessResponse<IEnumerable<Categories>>
      {
        Message = "Categories are returned succeefully",
        Data = categories
      });
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occured , can not return the category list");
      return StatusCode(500, new ErrorResponse
      {
        Message = ex.Message
      });
    }
  }


  [HttpGet("{category_id:guid}")]
  public async Task<IActionResult> GetCategory(string category_id)
  {
    try
    {
      if (!Guid.TryParse(category_id, out Guid categoryIdGuid))
      {
        return BadRequest("Invalid category ID Format");
      }
      var category = await _dbContext.GetCategoryById(categoryIdGuid);
      if (category == null)
      {
        return NotFound(new ErrorResponse
        {
          Message = $"No Category Found With ID : ({categoryIdGuid})"
        });
      }
      else
      {
        return Ok(new SuccessResponse<Categories>
        {
          Success = true,
          Message = "Category is returned succeefully",
          Data = category
        });
      }

    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occured , can not return the category");
      return StatusCode(500, new ErrorResponse
      {
        Message = ex.Message
      });
    }
  }


  [HttpPost]
  public async Task<IActionResult> CreateCategory(Categories newCategory)
  {
    try
    {
      var createdCategory = await _dbContext.CreateCategoryService(newCategory);
      if (createdCategory != null)
      {
        return CreatedAtAction(nameof(GetCategory), new
        {
          categoryId = createdCategory.category_id
        }, createdCategory);
      }

      return Ok(new SuccessResponse<Categories>
      {
        Success = true,
        Message = "Category is created succeefully",
        Data = createdCategory
      });
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occured , can not create new category");
      return StatusCode(500, new ErrorResponse
      {
        Message = ex.Message
      });
    }
  }

  [HttpPut("{category_id:guid}")]
  public async Task<IActionResult> UpdateCategory(string categoryId, Categories updateCategory)
  {
    try
    {
      if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
      {
        return BadRequest("Invalid category ID Format");
      }
      var category = await _dbContext.UpdateCategoryService(categoryIdGuid, updateCategory);
      if (category == null)
      {
        return NotFound(new ErrorResponse
        {
          Message = "No Category To Founed To Update"
        });
      }
      return Ok(new SuccessResponse<Categories>
      {
        Success = true,
        Message = "Category is updated succeefully",
        Data = category
      });
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occured , can not update the category");
      return StatusCode(500, new ErrorResponse
      {
        Message = ex.Message
      });
    }
  }


  [HttpDelete("{category_id:guid}")]
  public async Task<IActionResult> DeleteCategory(string categoryId)
  {
    try
    {
      if (!Guid.TryParse(categoryId, out Guid categoryIdGuid))
      {
        return BadRequest("Invalid Category ID Format");
      }
      var result = await _dbContext.DeleteCategoryService(categoryIdGuid);
      if (!result)
      {
        return NotFound(new ErrorResponse
        {
          Message = "The category is not found to be deleted"
        });
      }
      return Ok(new { success = true, message = "Category is deleted succeefully" });
    }
    catch (Exception ex)
    {
      Console.WriteLine($"An error occured , the category can not deleted");
      return StatusCode(500, new ErrorResponse
      {
        Message = ex.Message
      });
    }
  }
}