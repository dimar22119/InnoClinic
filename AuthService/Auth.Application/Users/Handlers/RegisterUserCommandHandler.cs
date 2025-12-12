using Auth.Application.Services;
using Auth.Application.Users.Commands;
using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Domain.ValueObjects;
using MediatR;

namespace Auth.Application.Users.Handlers
{
    public class RegisterUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Guid>
    {
        public async Task<Guid> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var existingUser = await userRepository.GetByEmailAsync(request.Email);
            if (existingUser is not null)
            {
                throw new InvalidOperationException("A user with that email already exists.");
            }

            var hashedPassword = passwordHasher.Hash(request.Password);

            var emailVo = Email.From(request.Email);
            var passwordHashVo = new PasswordHash(hashedPassword);

            var role = Enum.TryParse<Role>(request.Role, true, out var parsedRole) ? parsedRole : Role.Patient;

            var user = new User(emailVo, passwordHashVo, role);

            await userRepository.AddAsync(user);

            return user.Id;
        }
    }
}
