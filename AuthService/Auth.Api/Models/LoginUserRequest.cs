namespace Auth.Api.Models
{
    public sealed record LoginUserRequest(string Email, string Password);
}
