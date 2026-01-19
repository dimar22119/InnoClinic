namespace CatalogService.Domain.Entities
{
    public class Specialization
    {
        public Guid Id { get; init; }
        public required string Name { get; init; }
        public bool IsActive { get; init; }
    }
}
