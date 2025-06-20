namespace PcGear.Database.Dtos
{
    public class PagedResult<T>
    {
        public List<T> Data { get; set; } = new();
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage { get; set; }
        public bool HasNextPage { get; set; }
        public ProductFilterRequest AppliedFilters { get; set; } = new();

        public PagedResult() { }

        public PagedResult(List<T> data, int totalCount, int page, int pageSize, ProductFilterRequest filters)
        {
            Data = data;
            TotalCount = totalCount;
            Page = page;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling((double)totalCount / pageSize);
            HasPreviousPage = page > 1;
            HasNextPage = page < TotalPages;
            AppliedFilters = filters;
        }
    }
}