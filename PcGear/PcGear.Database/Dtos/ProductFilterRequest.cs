using PcGear.Database.Enums;

namespace PcGear.Database.Dtos
{
    public class ProductFilterRequest
    {
        
        public string? Name { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public List<int>? CategoryIds { get; set; }
        public List<int>? ManufacturerIds { get; set; }
        public bool? InStock { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedAfter { get; set; }
        public DateTime? CreatedBefore { get; set; }

        
        public ProductSortBy SortBy { get; set; } = ProductSortBy.Id;
        public SortDirection SortDirection { get; set; } = SortDirection.Ascending;

        
        private int _page = 1;
        private int _pageSize = 10;

        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;  
        }

        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = value < 1 ? 10 : (value > 100 ? 100 : value);  
        }
    }
}