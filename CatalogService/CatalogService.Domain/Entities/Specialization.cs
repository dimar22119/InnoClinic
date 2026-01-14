namespace CatalogService.Domain.Entities
{
    public class Specialization
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public bool IsActive { get; private set; }

        public Specialization(string name, bool isActive)
        {
            Id = Guid.NewGuid();
            Name = name;
            IsActive = isActive;
        }
    }
}
