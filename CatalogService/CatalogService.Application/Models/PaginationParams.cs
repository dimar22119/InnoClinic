namespace CatalogService.Application.Models
{
    public record PaginationParams
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }
        public int Skip => (PageNumber - 1) * PageSize;
        public int Take => PageSize;
        public PaginationParams(int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        }
    }
}
