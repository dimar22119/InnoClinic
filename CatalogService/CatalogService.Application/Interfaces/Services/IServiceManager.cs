using CatalogService.Application.Models.Services;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        Task<ServiceResponseDto> GetByIdAsync(Guid id);
        Task<IReadOnlyList<ServiceResponseDto>> GetAllAsync();
        Task<ServiceResponseDto> CreateAsync(CreateServiceDto dto);
        Task UpdateAsync(Guid id, UpdateServiceDto dto);
        Task DeleteAsync(Guid id);
    }
}
