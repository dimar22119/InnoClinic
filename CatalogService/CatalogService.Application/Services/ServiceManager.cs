using CatalogService.Application.Dtos.Services;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Services
{
    public class ServiceManager(IServiceRepository repo, ISpecializationRepository specializationRepo) : IServiceManager
    {
        public Task<Service?> GetByIdAsync(Guid id)
            => repo.GetByIdAsync(id);

        public Task<IEnumerable<Service>> GetAllAsync()
            => repo.GetAllAsync();

        public async Task<Service> CreateAsync(CreateServiceDto dto)
        {
            var specialization = await specializationRepo.GetByIdAsync(dto.SpecializationId)
                ?? throw new Exception("Specialization not found");

            var service = new Service(
                Guid.NewGuid(),
                dto.SpecializationId,
                dto.Name,
                dto.Price,
                dto.IsActive,
                dto.Category
            );

            await repo.AddAsync(service);
            return service;
        }

        public async Task UpdateAsync(Guid id, UpdateServiceDto dto)
        {
            var existing = await repo.GetByIdAsync(id)
                ?? throw new Exception("Service not found");

            var updated = new Service(
                existing.Id,
                existing.SpecializationId,
                dto.Name,
                dto.Price,
                dto.IsActive,
                dto.Category
            );

            await repo.UpdateAsync(updated);
        }

        public Task DeleteAsync(Guid id)
            => repo.DeleteAsync(id);
    }

}
