using PcGear.Core.Dtos.BaseDtos.Categories;
using PcGear.Core.Dtos.Requests;
using PcGear.Database.Entities;

namespace PcGear.Core.Mapping
{
    public static class CategoriesMappingExtensions
    {
        public static Category ToEntity(this AddCategoryRequest request)
        {
            return new Category
            {
                Name = request.Name,
                Description = request.Description,
                CreatedAt = DateTime.UtcNow
            };
        }


        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description
            };
        }

        public static List<CategoryDto> ToCategoryDtos(this List<Category> categories)
        {
            return categories.Select(c => c.ToCategoryDto()).ToList();
        }
    }
}
