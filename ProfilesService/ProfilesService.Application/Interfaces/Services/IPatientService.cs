using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Patients;

namespace ProfilesService.Application.Interfaces.Services
{
    public interface IPatientService
    {
        Task<Result<PatientResponse>> GetByIdAsync(Guid id);
        Task<Result<IEnumerable<PatientResponse>>> GetAllAsync();
        Task<Result<PatientResponse>> CreateAsync(CreatePatientRequest request);
        Task<Result<PatientResponse>> UpdateAsync(UpdatePatientRequest request);
        Task<Result> DeleteAsync(Guid id);
        Task<Result<IEnumerable<PatientResponse>>> GetByNameAsync(string firstName, string lastName);
    }
}
