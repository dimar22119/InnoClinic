using CatalogService.Domain.ValueObjects;

namespace CatalogService.Application.Dtos.Services
{
    public record CreateServiceDto(Guid SpecializationId, string Name, decimal Price, bool IsActive, ServiceCategory Category);
}
