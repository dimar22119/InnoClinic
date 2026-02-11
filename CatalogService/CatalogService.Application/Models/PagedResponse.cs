namespace CatalogService.Application.Models
{
    public record PagedResponse<T>(IReadOnlyList<T> Data, int PageNumber, int PageSize, int TotalRecords);
}
