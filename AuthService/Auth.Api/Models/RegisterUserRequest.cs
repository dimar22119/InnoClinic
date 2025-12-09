namespace Auth.Api.Models
{
    public sealed record RegisterUserRequest(string Email, string Password, string Role);
}
