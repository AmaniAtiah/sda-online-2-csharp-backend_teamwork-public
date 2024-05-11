using Microsoft.EntityFrameworkCore;
using Backend.EntityFramework;
using Backend.Helpers;
using Backend.Models;
using Backend.Dtos;
using Backend.Dtos.Pagination;
using AutoMapper;

namespace Backend.Services
{
    public class CategoryService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public CategoryService(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<PaginationResult<CategoryDto>> GetAllCategoryAsync(int pageNumber, int pageSize)
        {
            try
            {
                var totalCategoryCount = await _appDbContext.Categories.CountAsync();

                var categories = await _appDbContext.Categories
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .ToListAsync();

                var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

                return new PaginationResult<CategoryDto>
                {
                    Items = categoryDtos,
                    TotalCount = totalCategoryCount,
                    PageNumber = pageNumber,
                    PageSize = pageSize
                };
            }
            catch (Exception e)
            {
                throw new ApplicationException("An error occurred while retrieving categories.", e);
            }
        }

        public async Task<CategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _appDbContext.Categories.FindAsync(categoryId);
            var categoryDto = _mapper.Map<CategoryDto>(category);
            return categoryDto;
        }

        public async Task<CategoryDto> AddCategoryAsync(CategoryDto newCategory)
        {
            var category = new Category
            {
                Name = newCategory.Name,
                Description = newCategory.Description,
            };
            _appDbContext.Categories.Add(category);
            await _appDbContext.SaveChangesAsync();
            var newCategoryDto = new CategoryDto
            {
                CategoryId = category.CategoryId,
                Name = category.Name,
                Description = category.Description,
            };
            return newCategoryDto;
        }

        public async Task<Category?> UpdateCategoryAsync(Guid categoryId, CategoryDto updateCategory)
        {
            var existingCategory = await _appDbContext.Categories.FindAsync(categoryId);
            if (existingCategory != null)
            {
                existingCategory.Name = updateCategory.Name ?? existingCategory.Name;
                existingCategory.Slug = SlugResponse.GenerateSlug(existingCategory.Name);
                existingCategory.Description = updateCategory.Description ?? existingCategory.Description;
                await _appDbContext.SaveChangesAsync();
            }
            return existingCategory;
        }

        public async Task<bool> DeleteCategoryAsync(Guid categoryId)
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