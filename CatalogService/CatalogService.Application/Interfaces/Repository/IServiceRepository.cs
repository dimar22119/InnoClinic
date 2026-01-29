using CatalogService.Domain.Entities;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<PagedList<Service>> GetPagedAsync(PaginationParams paginationParams);
        Task AddAsync(Service service);
        Task UpdateAsync(Service service);
        Task DeleteAsync(Service service);
    }
}
