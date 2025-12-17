namespace ProfilesService.Application.Dtos.Receptionist
{
    public sealed record ReceptionistResponse(
        Guid Id,
        Guid AccountId,
        string FirstName,
        string LastName
    );
}
