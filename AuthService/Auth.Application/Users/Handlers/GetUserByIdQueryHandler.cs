using Auth.Application.Users.Dto;
using Auth.Application.Users.Queries;
using Auth.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Users.Handlers
{
    public class GetUserByIdQueryHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user is null) return null;

            return new UserDto(user.Id, user.Email.Value, user.CreatedAt);
        }
    }
}
