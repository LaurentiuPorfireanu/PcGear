using PcGear.Database.Dtos;
using PcGear.Database.Entities;
using PcGear.Database.Enums;

namespace PcGear.Database.QueryExtensions
{
    public static class ProductQueryExtensions
    {
        public static IQueryable<Product> ApplyFilters(this IQueryable<Product> query, ProductFilterRequest filter)
        {
            if (filter == null)
                return query;

          
            if (!string.IsNullOrWhiteSpace(filter.Name))
            {
                query = query.Where(p => p.Name.ToLower().Contains(filter.Name.ToLower()));
            }

            if (filter.MinPrice.HasValue)
            {
                query = query.Where(p => p.Price >= filter.MinPrice.Value);
            }

            if (filter.MaxPrice.HasValue)
            {
                query = query.Where(p => p.Price <= filter.MaxPrice.Value);
            }

            if (filter.CategoryIds != null && filter.CategoryIds.Any())
            {
                query = query.Where(p => filter.CategoryIds.Contains(p.CategoryId));
            }

            if (filter.ManufacturerIds != null && filter.ManufacturerIds.Any())
            {
                query = query.Where(p => filter.ManufacturerIds.Contains(p.ManufacturerId));
            }

            if (filter.InStock.HasValue)
            {
                if (filter.InStock.Value)
                {
                    query = query.Where(p => p.Stock > 0);
                }
                else
                {
                    query = query.Where(p => p.Stock == 0);
                }
            }

            if (!string.IsNullOrWhiteSpace(filter.Description))
            {
                query = query.Where(p => p.Description != null &&
                                        p.Description.ToLower().Contains(filter.Description.ToLower()));
            }

            if (filter.CreatedAfter.HasValue)
            {
                query = query.Where(p => p.CreatedAt >= filter.CreatedAfter.Value);
            }

            if (filter.CreatedBefore.HasValue)
            {
                query = query.Where(p => p.CreatedAt <= filter.CreatedBefore.Value);
            }

            return query;
        }


        public static IQueryable<Product> ApplySorting(this IQueryable<Product> query, ProductFilterRequest filter)
        {
            if (filter == null)
                return query.OrderBy(p => p.Id);

           
            return filter.SortBy switch
            {
                ProductSortBy.Id => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Id)
                    : query.OrderByDescending(p => p.Id),

                ProductSortBy.Name => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Name)
                    : query.OrderByDescending(p => p.Name),

                ProductSortBy.Price => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Price)
                    : query.OrderByDescending(p => p.Price),

                ProductSortBy.Stock => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Stock)
                    : query.OrderByDescending(p => p.Stock),

                ProductSortBy.CategoryName => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Category.Name)
                    : query.OrderByDescending(p => p.Category.Name),

                ProductSortBy.ManufacturerName => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.Manufacturer.Name)
                    : query.OrderByDescending(p => p.Manufacturer.Name),

                ProductSortBy.CreatedAt => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.CreatedAt)
                    : query.OrderByDescending(p => p.CreatedAt),

                ProductSortBy.ModifiedAt => filter.SortDirection == SortDirection.Ascending
                    ? query.OrderBy(p => p.ModifiedAt)
                    : query.OrderByDescending(p => p.ModifiedAt),

                _ => query.OrderBy(p => p.Id) 
            };
        }
        public static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, ProductFilterRequest filter)
        {
            if (filter == null)
                return query.Take(10);

            var skip = (filter.Page - 1) * filter.PageSize;
            return query.Skip(skip).Take(filter.PageSize);
        }

  
        public static IQueryable<Product> ApplyFiltersAndSortingAndPagination(this IQueryable<Product> query, ProductFilterRequest filter)
        {
        
            query = query.ApplyFilters(filter);

      
            query = query.ApplySorting(filter);

         
            query = query.ApplyPagination(filter);

            return query;
        }


     
        public static IQueryable<Product> ApplyFiltersAndSorting(this IQueryable<Product> query, ProductFilterRequest filter)
        {
         
            query = query.ApplyFilters(filter);

        
            query = query.ApplySorting(filter);

            return query;
        }


    }
}
