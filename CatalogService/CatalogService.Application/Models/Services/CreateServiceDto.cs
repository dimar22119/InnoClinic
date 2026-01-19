using CatalogService.Domain.ValueObjects;

namespace CatalogService.Application.Models.Services
{
    public record CreateServiceDto(Guid SpecializationId, string Name, decimal Price, bool IsActive, ServiceCategory Category);
}
