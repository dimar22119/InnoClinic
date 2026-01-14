using CatalogService.Domain.ValueObjects;

namespace CatalogService.Domain.Entities
{
    public class Service
    {
        public Guid Id { get; private set; }
        public Guid SpecializationId { get; private set; }
        public string Name { get; private set; }
        public decimal Price { get; private set; }
        public bool IsActive { get; private set; }
        public ServiceCategory Category { get; private set; }

        public Service(Guid id, Guid specializationId, string name, decimal price, bool isActive, ServiceCategory category)
        {
            Id = id;
            SpecializationId = specializationId;
            Name = name;
            Price = price;
            IsActive = isActive;
            Category = category;
        }
    }
}
