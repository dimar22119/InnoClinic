namespace CatalogService.Application.Models
{
    public record PaginationParams
    {
        public int PageNumber { get; init; }
        public int PageSize { get; init; }

        public PaginationParams(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber < 1 ? 1 : pageNumber;
            PageSize = pageSize < 1 ? 10 : (pageSize > 100 ? 100 : pageSize);
        }
    }
}
