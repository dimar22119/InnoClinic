using CatalogService.Domain.ValueObjects;

namespace CatalogService.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; init; }
        public Guid SpecializationId { get; init; }
        public required string Name { get; init; }
        public decimal Price { get; init; }
        public bool IsActive { get; init; }
        public ServiceCategory Category { get; init; }
    }
}
