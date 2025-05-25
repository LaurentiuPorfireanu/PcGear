using PcGear.Core.Dtos.BaseDtos.Manufacturers;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;


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
    }

}
