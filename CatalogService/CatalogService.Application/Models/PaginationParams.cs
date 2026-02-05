namespace CatalogService.Application.Models
{
    public record PaginationParams(int PageNumber = 1, int PageSize = 10)
    {
        public int PageNumber { get; init; } = PageNumber < 1 ? 1 : PageNumber;
        public int PageSize { get; init; } = PageSize < 1 ? 10 : (PageSize > 100 ? 100 : PageSize);
        internal int Skip => (PageNumber - 1) * PageSize;
        internal int Take => PageSize;
    }
}
