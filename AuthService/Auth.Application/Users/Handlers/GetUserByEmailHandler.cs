using Auth.Application.Users.Dto;
using Auth.Application.Users.Queries;
using Auth.Domain.Repositories;
using MediatR;

namespace Auth.Application.Users.Handlers
{
    public class GetUserByEmailHandler(IUserRepository userRepository) : IRequestHandler<GetUserByEmailQuery, UserDto?>
    {
        public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellation)
        {
            var user = await userRepository.GetByEmailAsync(request.Email);
            if(user is null) return null;

            return new UserDto(user.Id, user.Email.Value, user.CreatedAt, user.Role.ToString());
        }
    }
}
