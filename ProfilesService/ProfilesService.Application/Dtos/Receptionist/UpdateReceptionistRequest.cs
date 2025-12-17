namespace ProfilesService.Application.Dtos.Receptionist
{
    public sealed record UpdateReceptionistRequest(
        Guid Id,
        string FirstName,
        string LastName
    );
}
