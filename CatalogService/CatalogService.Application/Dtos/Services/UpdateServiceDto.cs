using CatalogService.Domain.ValueObjects;

namespace CatalogService.Application.Dtos.Services
{
    public record UpdateServiceDto(string Name, decimal Price, bool IsActive, ServiceCategory Category);
}
