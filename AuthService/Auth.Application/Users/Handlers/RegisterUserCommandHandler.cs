using Auth.Application.Services;
using Auth.Application.Users.Commands;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Domain.ValueObjects;
using MediatR;

namespace Auth.Application.Users.Handlers
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                throw new InvalidOperationException("A user with that email already exists.");
            }

            var hashedPassword = _passwordHasher.Hash(request.Password);

            var emailVo = Email.From(request.Email);
            var passwordHashVo = new PasswordHash(hashedPassword);

            var user = new User(emailVo, passwordHashVo);

            await _userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
