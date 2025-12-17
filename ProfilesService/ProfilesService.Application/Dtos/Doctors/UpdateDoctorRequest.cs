using ProfilesService.Domain.ValueObjects;

namespace ProfilesService.Application.Dtos.Doctors
{
    public sealed record UpdateDoctorRequest(
        Guid Id,
        string FirstName,
        string LastName,
        int CareerYearStart,
        DoctorStatus Status
    );
}
