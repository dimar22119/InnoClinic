using CatalogService.Application.Dtos.Services;
using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Models.Specializations;
using CatalogService.Domain.Entities;
using Mapster;

namespace CatalogService.Application.Services
{
    public class SpecializationManager(ISpecializationRepository repo) : ISpecializationManager
    {
        public async Task<SpecializationResponseDto> GetByIdAsync(Guid id)
        {
            var item = await repo.GetByIdAsync(id)
                   ?? throw new NotFoundException($"Specialization with ID {id} was not found.");

            return item.Adapt<SpecializationResponseDto>();
        }

        public async Task<IReadOnlyList<SpecializationResponseDto>> GetAllAsync()
        {
            var items = await repo.GetAllAsync();
            return items.Adapt<IReadOnlyList<SpecializationResponseDto>>();
        }

        public async Task<SpecializationResponseDto> CreateAsync(CreateSpecializationDto dto)
        {
            var specialization = new Specialization
            {
                Name = dto.Name,
                IsActive = dto.IsActive
            };

            await repo.AddAsync(specialization);
            return specialization.Adapt<SpecializationResponseDto>();
        }

        public async Task UpdateAsync(Guid id, UpdateSpecializationDto dto)
        {
            var specialization = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Specialization with ID {id} was not found.");

            var updated = new Specialization
            {
                Name = dto.Name,
                IsActive = dto.IsActive
            };

            await repo.UpdateAsync(updated);
        }

        public async Task DeleteAsync(Guid id)
        {
            var specialization = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Specialization with ID {id} was not found.");

            await repo.DeleteAsync(specialization);
        }
    }

}
