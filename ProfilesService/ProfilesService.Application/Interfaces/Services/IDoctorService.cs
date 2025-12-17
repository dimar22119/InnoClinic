using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Doctors;

namespace ProfilesService.Application.Interfaces.Services
{
    public interface IDoctorService
    {
        Task<Result<DoctorResponse>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<DoctorResponse>>> GetAllAsync();
        Task<Result<DoctorResponse>> CreateAsync(CreateDoctorRequest request);
        Task<Result<DoctorResponse>> UpdateAsync(UpdateDoctorRequest request); 
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<DoctorResponse>>> GetByNameAsync(string firstName, string lastName);

    }
}
