using CatalogService.Domain.Entities;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface IServiceRepository
    {
        Task<Service?> GetByIdAsync(Guid id);
        Task<PagedList<Service>> GetPagedAsync(int skip, int take);
        void Add(Service service);
        void Update(Service service);
        void Delete(Service service);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
