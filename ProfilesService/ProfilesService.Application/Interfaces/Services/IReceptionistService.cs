using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Receptionist;

namespace ProfilesService.Application.Interfaces.Services
{
    public interface IReceptionistService
    {
        Task<Result<ReceptionistResponse>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<ReceptionistResponse>>> GetAllAsync();
        Task<Result<ReceptionistResponse>> CreateAsync(CreateReceptionistRequest request);
        Task<Result<ReceptionistResponse>> UpdateAsync(UpdateReceptionistRequest request);
        Task<Result> DeleteAsync(Guid id);
    }
}
