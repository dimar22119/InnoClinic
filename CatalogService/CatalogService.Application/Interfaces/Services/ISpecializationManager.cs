using CatalogService.Application.Dtos.Specializations;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Services
{
    public interface ISpecializationManager
    {
        Task<Specialization?> GetByIdAsync(Guid id);
        Task<IEnumerable<Specialization>> GetAllAsync();
        Task<Specialization> CreateAsync(CreateSpecializationDto dto);
        Task UpdateAsync(Guid id, UpdateSpecializationDto dto);
        Task DeleteAsync(Guid id);
    }
}
