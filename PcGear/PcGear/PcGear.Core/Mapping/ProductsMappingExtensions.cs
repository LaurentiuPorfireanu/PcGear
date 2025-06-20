using PcGear.Core.Dtos.BaseDtos.Products;
using PcGear.Core.Dtos.Requests;
using PcGear.Core.Dtos.Responses;
using PcGear.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcGear.Core.Mapping
{
    public static class ProductsMappingExtensions
    {
        public static Product ToEntity(this AddProductRequest request)
        {
            return new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                CategoryId = request.CategoryId,
                ManufacturerId = request.ManufacturerId,
                CreatedAt = DateTime.UtcNow
            };
        }


        public static ProductDto ToProductDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryName = product.Category?.Name ?? "",
                ManufacturerName = product.Manufacturer?.Name ?? ""
            };
        }


        public static List<ProductDto> ToProductDtos(this List<Product> products)
        {
            return products.Select(p => p.ToProductDto()).ToList();
        }



        public static GetProductWithReviewsResponse ToGetProductWithReviewsResponse(this Product product)
        {
            var response = new GetProductWithReviewsResponse
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                CategoryName = product.Category?.Name ?? "",
                ManufacturerName = product.Manufacturer?.Name ?? "",
                Reviews = product.Reviews.Select(r => r.ToProductReviewDto()).ToList()
            };

            response.TotalReviews = response.Reviews.Count;
            response.AverageRating = response.Reviews.Any()
                ? response.Reviews.Average(r => r.Rating)
                : 0;

            return response;
        }

        public static ProductReviewDto ToProductReviewDto(this ProductReview review)
        {
            return new ProductReviewDto
            {
                Id = review.Id,
                Rating = review.Rating,
                ReviewText = review.ReviewText,
                UserName = $"{review.User?.FirstName} {review.User?.LastName}".Trim(),
                CreatedAt = review.CreatedAt
            };
        }


        public static void UpdateFromRequest(this Product product, UpdateProductRequest request)
        {
            product.Name = request.Name;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.CategoryId = request.CategoryId;
            product.ManufacturerId = request.ManufacturerId;
            product.ModifiedAt = DateTime.UtcNow;

        }


        public static void UpdateStockFromRequest(this Product product, UpdateProductStockRequest request)
        {
            product.Stock = request.Stock;
            product.ModifiedAt = DateTime.UtcNow;
        }
    }
}
