using FluentValidation;
using ProfilesService.Application.Dtos.Receptionist;

namespace ProfilesService.Application.Validators.Receptionists
{
    public class UpdateReceptionistRequestValidator : AbstractValidator<UpdateReceptionistRequest>
    {
        public UpdateReceptionistRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50);
        }
    }
}
