using Microsoft.EntityFrameworkCore;
using PcGear.Database.Context;
using PcGear.Database.Dtos;
using PcGear.Database.Entities;
using PcGear.Database.QueryExtensions;

namespace PcGear.Database.Repos
{
    public class ProductsRepository(PcGearDatabaseContext databaseContext) : BaseRepository<Product>(databaseContext)
    {

        public async Task<Product?> GetProductWithReviewsAsync(int productId)
        {
            return await databaseContext.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Include(p => p.Reviews)
                .ThenInclude(r => r.User)
                .Where(p => p.Id == productId && p.DeletedAt == null)
                .FirstOrDefaultAsync();
        }
        public async Task<List<Product>> GetAllWithDetailsAsync()
        {
            return await databaseContext.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => p.DeletedAt == null)
                .ToListAsync();
        }

        public async Task AddAsync(Product product)
        {
            databaseContext.Products.Add(product);
            await SaveChangesAsync();
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            return await databaseContext.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => p.Id == id && p.DeletedAt == null)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            product.ModifiedAt = DateTime.UtcNow;
            databaseContext.Products.Update(product);
            await SaveChangesAsync();
        }
        

        public async Task DeleteAsync(int id)
        {
            var product = await GetByIdAsync(id);
            if(product != null)
            {
                product.DeletedAt = DateTime.UtcNow;
                await UpdateAsync(product);

            }
        }

        public async Task<List<Product>> GetFilteredProductsAsync(ProductFilterRequest filter)
        {
            var query = databaseContext.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => p.DeletedAt == null);

            query = query.ApplyFiltersAndSorting(filter);  

            return await query.ToListAsync();
        }

        public async Task<int> GetFilteredProductsCountAsync(ProductFilterRequest filter)
        {
            var query = databaseContext.Products
                .Where(p => p.DeletedAt == null);

            query = query.ApplyFilters(filter);

            return await query.CountAsync();
        }

        public async Task<PagedResult<Product>> GetFilteredProductsPagedAsync(ProductFilterRequest filter)
        {
            var baseQuery = databaseContext.Products
                .Include(p => p.Category)
                .Include(p => p.Manufacturer)
                .Where(p => p.DeletedAt == null);

           
            var filteredQuery = baseQuery.ApplyFiltersAndSorting(filter);
            var totalCount = await filteredQuery.CountAsync();

            
            var pagedQuery = baseQuery.ApplyFiltersAndSortingAndPagination(filter);
            var products = await pagedQuery.ToListAsync();

            return new PagedResult<Product>(
                products,
                totalCount,
                filter?.ValidatedPage ?? 1,
                filter?.ValidatedPageSize ?? 10,
                filter ?? new ProductFilterRequest()
            );
        }



    }
}
