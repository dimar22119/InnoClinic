using FluentValidation;
using ProfilesService.Application.Dtos.Patients;

namespace ProfilesService.Application.Validators.Patients
{
    public class CreatePatientRequestValidator : AbstractValidator<CreatePatientRequest>
    {
        public CreatePatientRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50);

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");
        }
    }
}
