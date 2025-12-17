using Mapster;
using ProfilesService.Application.Common;
using ProfilesService.Application.Dtos.Receptionist;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Application.Interfaces.Services;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Application.Services
{
    public class ReceptionistService(IReceptionistRepository receptionistRepository) : IReceptionistService
    {
        public async Task<Result<ReceptionistResponse>> GetByIdAsync(Guid id)
        {
            var receptionist = await receptionistRepository.GetByIdAsync(id);
            return receptionist is null
                ? Result<ReceptionistResponse>.Failure(new Error(ErrorCode.NotFound, "Receptionist not found"))
                : Result<ReceptionistResponse>.Success(receptionist.Adapt<ReceptionistResponse>());
        }

        public async Task<Result<IEnumerable<ReceptionistResponse>>> GetAllAsync()
        {
            var receptionists = await receptionistRepository.GetAllAsync();
            return Result<IEnumerable<ReceptionistResponse>>.Success(receptionists.Adapt<IEnumerable<ReceptionistResponse>>());
        }

        public async Task<Result<ReceptionistResponse>> CreateAsync(CreateReceptionistRequest request)
        {
            var receptionist = new Receptionist(
                request.AccountId,
                request.FirstName,
                request.LastName
            );

            var existingReceptionist = await receptionistRepository.GetByNameAsync(request.FirstName, request.LastName);

            if(existingReceptionist != null)
            {
                return Result<ReceptionistResponse>.Failure(new Error(ErrorCode.Conflict, "Receptionist with the same name already exists"));
            }

            await receptionistRepository.AddAsync(receptionist);
            return Result<ReceptionistResponse>.Success(receptionist.Adapt<ReceptionistResponse>());
        }

        public async Task<Result<ReceptionistResponse>> UpdateAsync(UpdateReceptionistRequest request)
        {
            var receptionist = await receptionistRepository.GetByIdAsync(request.Id);
            if (receptionist is null)
                return Result<ReceptionistResponse>.Failure(new Error(ErrorCode.NotFound, "Receptionist not found"));

            receptionist.Update(
                request.FirstName,
                request.LastName
            );

            await receptionistRepository.UpdateAsync(receptionist);
            return Result<ReceptionistResponse>.Success(receptionist.Adapt<ReceptionistResponse>());
        }

        public async Task<Result> DeleteAsync(Guid id)
        {
            var receptionist = await receptionistRepository.GetByIdAsync(id);
            if (receptionist is null)
                return Result.Failure(new Error(ErrorCode.NotFound, "Receptionist not found"));

            await receptionistRepository.DeleteAsync(id);
            return Result.Success();
        }
    }
}
