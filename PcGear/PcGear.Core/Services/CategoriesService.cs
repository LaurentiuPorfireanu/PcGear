using PcGear.Core.Dtos.BaseDtos.Categories;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;
using PcGear.Infrastructure.Exceptions;

namespace PcGear.Core.Services
{
    public class CategoriesService(CategoriesRepository categoriesRepository)
    {
        public async Task AddCategoryAsync(AddCategoryRequest request)
        {
            var category = request.ToEntity();
            await categoriesRepository.AddAsync(category);
        }

        public async Task<List<CategoryDto>> GetAllCategoriesAsync()
        {
            var categories = await categoriesRepository.GetAllAsync();
            return categories.ToCategoryDtos();
        }

        public async Task<CategoryDto?> GetCategoryByIdAsync(int id)
        {
            var category = await categoriesRepository.GetByIdAsync(id);
            return category?.ToCategoryDto();
        }

        public async Task UpdateCategoryAsync(int id, UpdateCategoryRequest request)
        {
            var category = await categoriesRepository.GetByIdAsync(id);
            if (category == null)
                throw new ResourceMissingException("Category not found");

            category.Name = request.Name;
            category.Description = request.Description;

            await categoriesRepository.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(int id)
        {
            await categoriesRepository.DeleteAsync(id);
        }
    }
}