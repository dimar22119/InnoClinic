using Auth.Application.Services;
using Auth.Application.Users.Commands;
using Auth.Application.Users.Dto;
using Auth.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Users.Handlers
{
    internal class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher, ITokenService tokenService) : IRequestHandler<LoginUserCommand, LoginResultDto>
    {
        public async Task<LoginResultDto> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);
            if(user is null)
            {
                throw new InvalidOperationException("Invalid credentials.");
            }

            if(!passwordHasher.Verify(user.PasswordHash.Value, request.Password))
            {
                throw new InvalidOperationException("Invalid credentials.");
            }

            var role = user.Role.ToString();
            var token = tokenService.GenerateToken(user.Id, user.Email.Value, role);

            return new LoginResultDto(token);
        }
    }
}
