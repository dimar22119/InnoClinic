using Auth.Application.Users.Dto;
using MediatR;

namespace Auth.Application.Users.Commands
{
    public sealed record LoginUserCommand(string Email, string Password) : IRequest<LoginResultDto>;
}
