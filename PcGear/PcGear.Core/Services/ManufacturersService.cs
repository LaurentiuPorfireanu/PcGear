using PcGear.Core.Dtos.BaseDtos.Manufacturers;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;
using PcGear.Infrastructure.Exceptions;

namespace PcGear.Core.Services
{
    public class ManufacturersService(ManufacturersRepository manufacturersRepository)
    {
        public async Task AddManufacturerAsync(AddManufacturerRequest request)
        {
            var manufacturer = request.ToEntity();
            await manufacturersRepository.AddAsync(manufacturer);
        }

        public async Task<List<ManufacturerDto>> GetAllManufacturersAsync()
        {
            var manufacturers = await manufacturersRepository.GetAllAsync();
            return manufacturers.ToManufacturerDtos();
        }

        public async Task<ManufacturerDto?> GetManufacturerByIdAsync(int id)
        {
            var manufacturer = await manufacturersRepository.GetByIdAsync(id);
            return manufacturer?.ToManufacturerDto();
        }

        public async Task UpdateManufacturerAsync(int id, UpdateManufacturerRequest request)
        {
            var manufacturer = await manufacturersRepository.GetByIdAsync(id);
            if (manufacturer == null)
                throw new ResourceMissingException("Manufacturer not found");
            if (!string.IsNullOrWhiteSpace(request.Name))
                manufacturer.Name = request.Name;

            if (request.Country != null)
                manufacturer.Country = request.Country;

            if (request.Website != null)
                manufacturer.Website = request.Website;

            await manufacturersRepository.UpdateAsync(manufacturer);
        }

        public async Task DeleteManufacturerAsync(int id)
        {
            await manufacturersRepository.DeleteAsync(id);
        }
    }
}