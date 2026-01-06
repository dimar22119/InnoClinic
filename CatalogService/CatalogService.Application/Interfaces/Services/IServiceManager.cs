using CatalogService.Application.Dtos.Services;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service> CreateAsync(CreateServiceDto dto);
        Task UpdateAsync(Guid id, UpdateServiceDto dto);
        Task DeleteAsync(Guid id);
    }
}
