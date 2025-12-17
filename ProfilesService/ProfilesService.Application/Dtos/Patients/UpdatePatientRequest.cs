namespace ProfilesService.Application.Dtos.Patients
{
    public sealed record UpdatePatientRequest(
        Guid Id,
        string FirstName,
        string LastName,
        DateTime BirthDate
    );
}
