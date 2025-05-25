using PcGear.Core.Dtos.BaseDtos.Manufacturers;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Dtos.Requests;
using PcGear.Database.Entities;


namespace PcGear.Core.Mapping
{
    public static class ManufacturersMappingExtensions
    {
        public static Manufacturer ToEntity(this AddManufacturerRequest request)
        {
            return new Manufacturer
            {
                Name = request.Name,
                Country = request.Country,
                Website = request.Website,
                CreatedAt = DateTime.UtcNow
            };
        }

        public static ManufacturerDto ToManufacturerDto(this Manufacturer manufacturer)
        {
            return new ManufacturerDto
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name,
                Country = manufacturer.Country,
                Website = manufacturer.Website
            };
        }

        public static List<ManufacturerDto> ToManufacturerDtos(this List<Manufacturer> manufacturers)
        {
            return manufacturers.Select(m => m.ToManufacturerDto()).ToList();
        }
    }
}
