using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Repository
{
    public interface ISpecializationRepository
    {
        Task<Specialization?> GetByIdAsync(Guid id);
        Task<IReadOnlyList<Specialization>> GetAllAsync();
        Task AddAsync(Specialization specialization);
        Task UpdateAsync(Specialization specialization);
        Task DeleteAsync(Specialization specialization);
    }
}
