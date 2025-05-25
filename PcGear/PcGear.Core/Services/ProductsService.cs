using PcGear.Core.Dtos.BaseDtos.Products;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Dtos.Responses;
using PcGear.Core.Mapping;
using PcGear.Database.Repos;



namespace PcGear.Core.Services
{
    public class ProductsService(ProductsRepository productsRepository)
    {
        public async Task AddProductAsync(AddProductRequest request)
        {
            var product = request.ToEntity();
            await productsRepository.AddAsync(product);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await productsRepository.GetAllWithDetailsAsync();
            return products.ToProductDtos();
        }

        public async Task<GetProductWithReviewsResponse> GetProductWithReviewsAsync(int productId)
        {
            var product = await productsRepository.GetProductWithReviewsAsync(productId);

            if (product == null)
                throw new ArgumentException("Product not found");

            return product.ToGetProductWithReviewsResponse();
        }

        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var product = await productsRepository.GetByIdAsync(id);
            return product?.ToProductDto();
        }
        public async Task UpdateProductAsync(int id, UpdateProductRequest request)
        {
            var product = await productsRepository.GetByIdAsync(id);

            product.UpdateFromRequest(request);
            await productsRepository.UpdateAsync(product);
        }

        public async Task UpdateProductStockAsync(int id, UpdateProductStockRequest request)
        {
            var product = await productsRepository.GetByIdAsync(id);

            product.UpdateStockFromRequest(request);
            await productsRepository.UpdateAsync(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await productsRepository.GetByIdAsync(id);


            await productsRepository.DeleteAsync(id);
        }
    }
}
