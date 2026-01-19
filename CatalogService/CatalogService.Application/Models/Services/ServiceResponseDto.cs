using CatalogService.Domain.ValueObjects;

namespace CatalogService.Application.Models.Services
{
    public record ServiceResponseDto(Guid Id, Guid SpecializationId, string Name, decimal Price, bool IsActive, ServiceCategory Category);
}
