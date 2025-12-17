using ProfilesService.Domain.ValueObjects;

namespace ProfilesService.Application.Dtos.Doctors
{
    public sealed record CreateDoctorRequest(
        Guid AccountId,
        Guid SpecializationId,
        string FirstName,
        string LastName,
        int CareerYearStart,
        DoctorStatus Status
    );
}
