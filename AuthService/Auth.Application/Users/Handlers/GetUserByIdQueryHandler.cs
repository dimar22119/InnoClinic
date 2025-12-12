using Auth.Application.Users.Dto;
using Auth.Application.Users.Queries;
using Auth.Domain.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Users.Handlers
{
    public class GetUserByIdQueryHandler(IUserRepository userRepository) : IRequestHandler<GetUserByIdQuery, UserDto?>
    {
        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);
            if (user is null) return null;

            return new UserDto(user.Id, user.Email.Value, user.CreatedAt, user.Role.ToString());
        }
    }
}
