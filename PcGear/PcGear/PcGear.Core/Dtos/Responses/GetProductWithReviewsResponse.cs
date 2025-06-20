using PcGear.Core.Dtos.BaseDtos.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PcGear.Core.Dtos.Responses
{
    public class GetProductWithReviewsResponse:ProductDto
    {

        public List<ProductReviewDto> Reviews { get; set; } = [];
        public double AverageRating { get; set; }

        public int TotalReviews { get; set; }
    }
}
