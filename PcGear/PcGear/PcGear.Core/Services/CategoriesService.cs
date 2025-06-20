using PcGear.Core.Dtos.BaseDtos.Categories;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;


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
    }
}
