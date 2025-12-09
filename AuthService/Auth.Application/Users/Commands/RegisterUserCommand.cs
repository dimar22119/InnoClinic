using MediatR;

namespace Auth.Application.Users.Commands
{
    public sealed record RegisterUserCommand(string Email, string Password) : IRequest<Guid>;
}
