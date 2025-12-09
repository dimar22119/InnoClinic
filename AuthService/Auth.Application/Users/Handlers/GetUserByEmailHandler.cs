using Auth.Application.Users.Dto;
using Auth.Application.Users.Queries;
using Auth.Domain.Repositories;

namespace Auth.Application.Users.Handlers
{
    public class GetUserByEmailHandler
    {
        private readonly IUserRepository _userRepository;

        public GetUserByEmailHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto?> Handle(GetUserByEmailQuery request, CancellationToken cancellation)
        {
            var user = await _userRepository.GetByEmailAsync(request.Email);
            if(user is null) return null;

            return new UserDto(user.Id, user.Email.Value, user.CreatedAt);
        }
    }
}
