using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Interfaces.Services;
using CatalogService.Application.Models;
using CatalogService.Application.Models.Services;
using CatalogService.Domain.Entities;
using Mapster;

namespace CatalogService.Application.Services
{
    public class ServiceManager(IServiceRepository repo, ISpecializationRepository specializationRepo) : IServiceManager
    {
        public async Task<ServiceResponseDto> GetByIdAsync(Guid id)
        {
            var item = await repo.GetByIdAsync(id)
                   ?? throw new NotFoundException($"Service with ID {id} was not found.");

            return item.Adapt<ServiceResponseDto>();
        }

        public async Task<PagedResponse<ServiceResponseDto>> GetPagedAsync(PaginationParams paginationParams)
        {
            var pagedList = await repo.GetPagedAsync(paginationParams);

            var dtos = pagedList.Items.Adapt<IReadOnlyList<ServiceResponseDto>>();

            return new PagedResponse<ServiceResponseDto>(
                dtos,
                paginationParams.PageNumber,
                paginationParams.PageSize,
                pagedList.TotalCount);
        }

        public async Task<ServiceResponseDto> CreateAsync(CreateServiceDto dto)
        {
            var specialization = await specializationRepo.GetByIdAsync(dto.SpecializationId)
                ?? throw new NotFoundException("Specialization not found");

            var service = new Service
            {
                SpecializationId = dto.SpecializationId,
                Name = dto.Name,
                Price = dto.Price,
                IsActive = dto.IsActive,
                Category = dto.Category
            };

            await repo.AddAsync(service);
            return service.Adapt<ServiceResponseDto>();
        }

        public async Task UpdateAsync(Guid id, UpdateServiceDto dto)
        {
            var existing = await repo.GetByIdAsync(id)
                ?? throw new NotFoundException($"Service with ID {id} not found");

            var updated = new Service
            {
                Id = existing.Id,
                SpecializationId = existing.SpecializationId,
                Name = dto.Name,
                Price = dto.Price,
                IsActive = dto.IsActive,
                Category = dto.Category
            };

            await repo.UpdateAsync(updated);
        }

        public async Task DeleteAsync(Guid id)
        {
            var service = await repo.GetByIdAsync(id)
                  ?? throw new NotFoundException($"Service with ID {id} was not found.");

            await repo.DeleteAsync(service);
        }
    }

}
