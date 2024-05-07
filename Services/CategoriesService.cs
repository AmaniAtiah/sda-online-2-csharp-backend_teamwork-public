// using Microsoft.EntityFrameworkCore;
// using Backend.EntityFramework;
// using Backend.Helpers;
// using Backend.Services;

// namespace Backend.Services
// {
//   public class CategoriesService
//   {
//     private readonly AppDbContext _dbContext;
//     public CategoriesService(AppDbContext dbContext)
//     {
//       _dbContext = dbContext;
//     }

//     public async Task<IEnumerable<CategoryTable>> GetAllCategoryService()
//     {
//       return await _dbContext.Categories.ToListAsync();
//     }

//     public async Task<CategoryTable?> GetCategoryById(Guid category_id)
//     {
//       return await _dbContext.Categories.FindAsync(category_id);
//     }

//     public async Task<CategoryTable> CreateCategoryService(Categories newCategory)
//     {
//       newCategory.category_id = Guid.NewGuid();
//       newCategory.Slug = SlugResponse.GenerateSlug(newCategory.category_name);

//       var categoryTable = new CategoryTable
//       {
//         category_id = newCategory.category_id,
//       };

//       _dbContext.Categories.Add(categoryTable);
//       await _dbContext.SaveChangesAsync();

//       return categoryTable;
//     }

//     public async Task<CategoryTable?> UpdateCategoryService(Guid category_id, Categories updateCategory)
//     {
//       var existingCategory = await _dbContext.Categories.FindAsync(category_id);
//       if (existingCategory != null)
//       {
//         existingCategory.category_name = updateCategory.category_name ?? existingCategory.category_name;
//         existingCategory.description = updateCategory.description ?? existingCategory.category_name;
//         await _dbContext.SaveChangesAsync();
//       }
//       return existingCategory;
//     }

//     public async Task<bool> DeleteCategoryService(Guid category_id)
//     {
//       var categoryToRemove = await _dbContext.Categories.FindAsync(category_id);
//       if (categoryToRemove != null)
//       {
//         _dbContext.Categories.Remove(categoryToRemove);
//         await _dbContext.SaveChangesAsync();
//         return true;
//       }
//       return false;
//     }
//   }
// }