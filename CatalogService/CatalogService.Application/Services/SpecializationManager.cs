using CatalogService.Application.Dtos.Specializations;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services
{
    public class SpecializationManager(ISpecializationRepository repo) : ISpecializationManager
    {
        public Task<Specialization?> GetByIdAsync(Guid id)
            => repo.GetByIdAsync(id);

        public Task<IEnumerable<Specialization>> GetAllAsync()
            => repo.GetAllAsync();

        public async Task<Specialization> CreateAsync(CreateSpecializationDto dto)
        {
            var specialization = new Specialization(
                dto.Name,
                dto.IsActive
            );

            await repo.AddAsync(specialization);
            return specialization;
        }

        public async Task UpdateAsync(Guid id, UpdateSpecializationDto dto)
        {
            var specialization = await repo.GetByIdAsync(id)
                ?? throw new Exception("Specialization not found");

            var updated = new Specialization(
                dto.Name,
                dto.IsActive
            );

            await repo.UpdateAsync(updated);
        }

        public Task DeleteAsync(Guid id)
            => repo.DeleteAsync(id);
    }

}
