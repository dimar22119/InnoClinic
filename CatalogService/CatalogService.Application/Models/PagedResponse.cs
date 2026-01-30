namespace CatalogService.Application.Models
{
    public record PagedResponse<T>(IReadOnlyList<T> Data, int PageNumber, int PageSize, int TotalRecords)
    {
        public int TotalPages => TotalRecords == 0
            ? 0
            : (int)Math.Ceiling(TotalRecords / (double)PageSize);
    }
}
