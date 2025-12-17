namespace ProfilesService.Application.Dtos.Receptionist
{
    public sealed record CreateReceptionistRequest(
        Guid AccountId,
        string FirstName,
        string LastName
    );
}
