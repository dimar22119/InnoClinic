using Mapster;
using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Patients;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Application.Interfaces.Services;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Services
{
    public class PatientService(IPatientRepository patientRepository) : IPatientService
    {
        public async Task<Result<PatientResponse>> GetByIdAsync(Guid id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            return patient is null
                ? Result<PatientResponse>.Failure(new Error(ErrorCode.NotFound, "Patient not found"))
                : Result<PatientResponse>.Success(patient.Adapt<PatientResponse>());
        }

        public async Task<Result<IEnumerable<PatientResponse>>> GetAllAsync()
        {
            var patients = await patientRepository.GetAllAsync();
            return Result<IEnumerable<PatientResponse>>.Success(patients.Adapt<IEnumerable<PatientResponse>>());
        }

        public async Task<Result<PatientResponse>> CreateAsync(CreatePatientRequest request)
        {
            var patient = new Patient(
                request.AccountId,
                request.FirstName,
                request.LastName,
                request.BirthDate
            );

            var existingPatient = await patientRepository.GetByNameAsync(request.FirstName, request.LastName);

            if(existingPatient != null)
            {
                return Result<PatientResponse>.Failure(new Error(ErrorCode.Conflict, "Patient with the same name already exists"));
            }

            await patientRepository.AddAsync(patient);
            return Result<PatientResponse>.Success(patient.Adapt<PatientResponse>());
        }

        public async Task<Result<PatientResponse>> UpdateAsync(UpdatePatientRequest request)
        {
            var patient = await patientRepository.GetByIdAsync(request.Id);
            if (patient is null)
                return Result<PatientResponse>.Failure(new Error(ErrorCode.NotFound, "Patient not found"));

            patient.Update(
                request.FirstName,
                request.LastName,
                request.BirthDate
            );

            await patientRepository.UpdateAsync(patient);
            return Result<PatientResponse>.Success(patient.Adapt<PatientResponse>());
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var patient = await patientRepository.GetByIdAsync(id);
            if (patient is null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Patient not found"));

            await patientRepository.DeleteAsync(id);
            return Result.Success();
        }

        public async Task<Result<IEnumerable<PatientResponse>>> GetByNameAsync(string firstName, string lastName)
        {
            var patients = await patientRepository.GetByNameAsync(firstName, lastName);
            return Result<IEnumerable<PatientResponse>>.Success(patients.Adapt<IEnumerable<PatientResponse>>());
        }
    }
}
