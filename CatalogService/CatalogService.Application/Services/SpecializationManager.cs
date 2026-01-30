using CatalogService.Application.Models;
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

        public async Task<PagedResponse<SpecializationResponseDto>> GetPagedAsync(PaginationParams paginationParams)
        {
            var pagedList = await repo.GetPagedAsync(paginationParams.Skip, paginationParams.Take);

            var dtos = pagedList.Adapt<IReadOnlyList<SpecializationResponseDto>>();

            return new PagedResponse<SpecializationResponseDto>(
                dtos,
                paginationParams.PageNumber,
                paginationParams.PageSize,
                pagedList.TotalCount);
        }

        public async Task<SpecializationResponseDto> CreateAsync(CreateSpecializationDto dto)
        {
            var specialization = new Specialization
            {
                Name = dto.Name,
                IsActive = dto.IsActive
            };

            repo.Add(specialization);
            await repo.SaveChangesAsync();
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

            repo.Update(updated);
            await repo.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var specialization = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Specialization with ID {id} was not found.");

            repo.Delete(specialization);
            await repo.SaveChangesAsync();
        }
    }

}
