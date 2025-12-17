using FluentValidation;
using ProfilesService.Application.Dtos.Patients;
using ProfilesService.Domain.Common;

namespace ProfilesService.Application.Validators.Patients
{
    public class UpdatePatientRequestValidator : AbstractValidator<UpdatePatientRequest>
    {
        public UpdatePatientRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FieldConstraints.FirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(FieldConstraints.LastNameMaxLength);

            RuleFor(x => x.BirthDate)
                .LessThan(DateTime.UtcNow).WithMessage("Birth date must be in the past.");
        }
    }
}
