using FluentValidation;
using ProfilesService.Application.Dtos.Receptionist;
using ProfilesService.Domain.Common;

namespace ProfilesService.Application.Validators.Receptionists
{
    public class CreateReceptionistRequestValidator : AbstractValidator<CreateReceptionistRequest>
    {
        public CreateReceptionistRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FieldConstraints.FirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(FieldConstraints.LastNameMaxLength);
        }
    }
}
