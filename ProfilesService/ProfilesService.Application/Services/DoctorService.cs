using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Doctors;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Application.Interfaces.Services;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Services
{
    public class DoctorService(IDoctorRepository doctorRepository) : IDoctorService
    {
        public async Task<Result<DoctorResponse>> GetByIdAsync(Guid id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);
            return doctor is null
                ? Result<DoctorResponse>.Failure(new Error(ErrorCode.NotFound, "Doctor not found"))
                : Result<DoctorResponse>.Success(MapToResponse(doctor));
        }

        public async Task<Result<IEnumerable<DoctorResponse>>> GetAllAsync()
        {
            var doctors = await doctorRepository.GetAllAsync();
            return Result<IEnumerable<DoctorResponse>>.Success(doctors.Select(MapToResponse));
        }

        public async Task<Result<DoctorResponse>> CreateAsync(CreateDoctorRequest request)
        {
            var doctor = new Doctor(
                request.AccountId,
                request.SpecializationId,
                request.FirstName,
                request.LastName,
                request.CareerYearStart,
                request.Status
            );

            await doctorRepository.AddAsync(doctor);
            return Result<DoctorResponse>.Success(MapToResponse(doctor));
        }

        public async Task<Result<DoctorResponse>> UpdateAsync(UpdateDoctorRequest request)
        {
            var doctor = await doctorRepository.GetByIdAsync(request.Id);
            if (doctor is null) 
                return Result<DoctorResponse>.Failure(new Error(ErrorCode.NotFound, "Doctor not found"));

            doctor.Update(
                request.FirstName,
                request.LastName,
                request.CareerYearStart,
                request.Status
            );

            await doctorRepository.UpdateAsync(doctor);
            return Result<DoctorResponse>.Success(MapToResponse(doctor));
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var doctor = await doctorRepository.GetByIdAsync(id);
            if (doctor is null) 
                return Result.Failure(new Error(ErrorCode.NotFound, "Doctor not found"));

            await doctorRepository.DeleteAsync(id);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<DoctorResponse>>> GetByNameAsync(string firstName, string lastName)
        {
            var doctors = await doctorRepository.GetByNameAsync(firstName, lastName);
            return Result<IEnumerable<DoctorResponse>>.Success(doctors.Select(MapToResponse));
        }

        private static DoctorResponse MapToResponse(Doctor doctor) =>
            new DoctorResponse(
                doctor.Id,
                doctor.AccountId,
                doctor.SpecializationId,
                doctor.FirstName,
                doctor.LastName,
                doctor.CareerYearStart,
                doctor.Status
            );
    }
}
