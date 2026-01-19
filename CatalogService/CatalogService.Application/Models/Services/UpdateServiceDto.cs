using CatalogService.Domain.ValueObjects;

namespace CatalogService.Application.Models.Services
{
    public record UpdateServiceDto(string Name, decimal Price, bool IsActive, ServiceCategory Category);
}
