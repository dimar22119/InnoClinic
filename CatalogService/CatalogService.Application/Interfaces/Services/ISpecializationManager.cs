using CatalogService.Application.Models.Specializations;
using CatalogService.Application.Models;

namespace CatalogService.Application.Interfaces.Services
{
    public interface ISpecializationManager
    {
        Task<SpecializationResponseDto> GetByIdAsync(Guid id);
        Task<PagedResponse<SpecializationResponseDto>> GetPagedAsync(PaginationParams paginationParams);
        Task<SpecializationResponseDto> CreateAsync(CreateSpecializationDto dto);
        Task UpdateAsync(Guid id, UpdateSpecializationDto dto);
        Task DeleteAsync(Guid id);
    }
}
