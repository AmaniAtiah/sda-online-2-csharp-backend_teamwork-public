using Microsoft.EntityFrameworkCore;
using api.Data;
using api.Helpers;

namespace api.Services;

public class CategoriesService
{

  private readonly AppDbContext _dbContext;
  public CategoriesService(AppDbContext dbContext)
  {
    _dbContext = dbContext;
  }


  public async Task<IEnumerable<Categories>> GetAllCategoryService()
  {
    return await _dbContext.Categories.ToListAsync();
  }


  public async Task<Categories?> GetCategoryById(Guid category_id)
  {
    return await _dbContext.Categories.FindAsync(category_id);
  }


  public async Task<Categories> CreateCategoryService(Categories newCategory)
  {
    newCategory.category_id = Guid.NewGuid();
    await _dbContext.SaveChangesAsync();
    return newCategory;
  }


  public async Task<Categories?> UpdateCategoryService(Guid category_id, Categories updateCategory)
  {
    var existingCategory = await _dbContext.Categories.FindAsync(category_id);
    if (existingCategory != null)
    {
      existingCategory.category_name = updateCategory.category_name ?? existingCategory.category_name;
      existingCategory.description = updateCategory.description ?? existingCategory.category_name;
      await _dbContext.SaveChangesAsync();
    }
    return existingCategory;
  }


  public async Task<bool> DeleteCategoryService(Guid category_id)
  {
    var categoryToRemove = await _dbContext.Categories.FindAsync(category_id);
    if (categoryToRemove != null)
    {
      _dbContext.Categories.Remove(categoryToRemove);
      await _dbContext.SaveChangesAsync();
      return true;
    }
    return false;
  }

}