using CatalogService.Application.Models.Specializations;
using CatalogService.Domain.Entities;

namespace CatalogService.Application.Interfaces.Services
{
    public interface ISpecializationManager
    {
        Task<SpecializationResponseDto> GetByIdAsync(Guid id);
        Task<IReadOnlyList<SpecializationResponseDto>> GetAllAsync();
        Task<SpecializationResponseDto> CreateAsync(CreateSpecializationDto dto);
        Task UpdateAsync(Guid id, UpdateSpecializationDto dto);
        Task DeleteAsync(Guid id);
    }
}
