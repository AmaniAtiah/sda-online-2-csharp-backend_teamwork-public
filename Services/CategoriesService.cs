using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;

namespace Backend.Services
{
  public class CategoriesService
  {
    private readonly AppDbContext _appDbContext;
    public CategoriesService(AppDbContext appDbContext)
    {
      _appDbContext = appDbContext;
    }

    public async Task<IEnumerable<Categories>> GetAllCategoryService()
    {
      return await _appDbContext.Categories.ToListAsync();
    }

    public async Task<Categories?> GetCategoryById(Guid category_id)
    {
      return await _appDbContext.Categories.FindAsync(category_id);
    }

    public async Task<Categories> CreateCategoryService(Categories newCategory)
{
    var categories = new Categories
    {
        category_id = Guid.NewGuid(),
        category_name = newCategory.category_name,
        Slug = SlugResponse.GenerateSlug(newCategory.category_name),
        description = newCategory.description
    };

    _appDbContext.Categories.Add(newCategory);
    await _appDbContext.SaveChangesAsync();

    return newCategory;
}


    public async Task<Categories?> UpdateCategoryService(Guid category_id, Categories updateCategory)
    {
      var existingCategory = await _appDbContext.Categories.FindAsync(category_id);
      if (existingCategory != null)
      {
        existingCategory.category_name = updateCategory.category_name ?? existingCategory.category_name;
        existingCategory.description = updateCategory.description ?? existingCategory.category_name;
        await _appDbContext.SaveChangesAsync();
      }
      return existingCategory;
    }

    public async Task<bool> DeleteCategoryService(Guid category_id)
    {
      var categoryToRemove = await _appDbContext.Categories.FindAsync(category_id);
      if (categoryToRemove != null)
      {
        _appDbContext.Categories.Remove(categoryToRemove);
        await _appDbContext.SaveChangesAsync();
        return true;
      }
      return false;
    }
  }
}