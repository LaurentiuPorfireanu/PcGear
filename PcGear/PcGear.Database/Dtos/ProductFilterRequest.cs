using PcGear.Database.Enums;

namespace PcGear.Database.Dtos
{
    public class ProductFilterRequest
    {
      
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? MinStock { get; set; }
        public int? MaxStock { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? ManufacturerIds { get; set; }
        public bool? InStock { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }

 
        public ProductSortBy SortBy { get; set; } = ProductSortBy.Id;
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

 
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;

      
        public int ValidatedPage => Page < 1 ? 1 : Page;
        public int ValidatedPageSize => PageSize < 1 ? 10 : (PageSize > 100 ? 100 : PageSize);
    }
}