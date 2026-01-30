using CatalogService.Domain.Entities;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<Specialization?> GetByIdAsync(Guid id);
        Task<PagedList<Specialization>> GetPagedAsync(int skip, int take);
        void Add(Specialization specialization);
        void Update(Specialization specialization);
        void Delete(Specialization specialization);
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
