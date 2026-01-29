using CatalogService.Domain.Entities;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<Specialization?> GetByIdAsync(Guid id);
        Task<PagedList<Specialization>> GetPagedAsync(PaginationParams paginationParams);
        Task AddAsync(Specialization specialization);
        Task UpdateAsync(Specialization specialization);
        Task DeleteAsync(Specialization specialization);
    }
}
