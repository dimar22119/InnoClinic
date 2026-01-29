using CatalogService.Application.Models.Services;
using CatalogService.Application.Models;

namespace CatalogService.Application.Interfaces.Services
{
    public interface IServiceManager
    {
        Task<ServiceResponseDto> GetByIdAsync(Guid id);
        Task<PagedResponse<ServiceResponseDto>> GetPagedAsync(PaginationParams paginationParams);
        Task<ServiceResponseDto> CreateAsync(CreateServiceDto dto);
        Task UpdateAsync(Guid id, UpdateServiceDto dto);
        Task DeleteAsync(Guid id);
    }
}
