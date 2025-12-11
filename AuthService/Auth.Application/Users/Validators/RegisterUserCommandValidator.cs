using Auth.Application.Users.Commands;
using Auth.Domain.ValueObjects;
using FluentValidation;

namespace Auth.Application.Users.Validators
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .WithMessage("A valid email is required.");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.Role)
                .NotEmpty()
                .Must(role => Enum.TryParse<Role>(role, true, out _))
                .WithMessage("Role must be one of: Admin, Doctor, User");
        }
    }
}
