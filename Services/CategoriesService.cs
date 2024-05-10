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

    public async Task<IEnumerable<Category>> GetAllCategoryService()
    {
      return await _appDbContext.Categories.Include(c => c.Products).ToListAsync();
    }

    public async Task<Category?> GetCategoryById(Guid categoryId)
    {
      return await _appDbContext.Categories.FindAsync(categoryId);
    }

    public async Task<Category> AddCategoryService(Category newCategory)
    {
      var categories = new Category
      {
        CategoryId = Guid.NewGuid(),
        category_name = newCategory.category_name,
        Slug = SlugResponse.GenerateSlug(newCategory.category_name),
        Description = newCategory.Description
      };

      _appDbContext.Categories.Add(newCategory);
      await _appDbContext.SaveChangesAsync();

      return newCategory;
    }
<<<<<<< HEAD
    public async Task<Categories?> UpdateCategoryService(Guid category_id, Categories updateCategory)
=======


    public async Task<Category?> UpdateCategoryService(Guid categoryId, Category updateCategory)
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
    {
      var existingCategory = await _appDbContext.Categories.FindAsync(categoryId);
      if (existingCategory != null)
      {
        existingCategory.category_name = updateCategory.category_name ?? existingCategory.category_name;
        existingCategory.Description = updateCategory.Description ?? existingCategory.category_name;
        await _appDbContext.SaveChangesAsync();
      }
      return existingCategory;
    }
<<<<<<< HEAD
    public async Task<bool> DeleteCategoryService(Guid category_id)
=======

    public async Task<bool> DeleteCategoryService(Guid categoryId)
>>>>>>> a2f2879185d485590f8e73d13c7aded13d24c182
    {
      var categoryToRemove = await _appDbContext.Categories.FindAsync(categoryId);
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

