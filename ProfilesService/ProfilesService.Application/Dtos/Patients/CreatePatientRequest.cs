namespace ProfilesService.Application.Dtos.Patients
{
    public sealed record CreatePatientRequest(
        Guid AccountId,
        string FirstName,
        string LastName,
        DateTime BirthDate
    );
}
