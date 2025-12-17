namespace ProfilesService.Application.Dtos.Patients
{
    public sealed record PatientResponse(
        Guid Id,
        Guid AccountId,
        string FirstName,
        string LastName,
        DateTime BirthDate
    );
}
