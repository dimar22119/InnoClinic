using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Guid id);
    }
}
