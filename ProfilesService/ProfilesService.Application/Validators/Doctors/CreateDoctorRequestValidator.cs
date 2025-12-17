using FluentValidation;
using ProfilesService.Application.Dtos.Doctors;
using ProfilesService.Domain.Common;

namespace ProfilesService.Application.Validators.Doctors
{
    public class CreateDoctorRequestValidator : AbstractValidator<CreateDoctorRequest>
    {
        public CreateDoctorRequestValidator()
        {
            RuleFor(x => x.AccountId).NotEmpty();

            RuleFor(x => x.SpecializationId).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(FieldConstraints.FirstNameMaxLength);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(FieldConstraints.LastNameMaxLength);

            RuleFor(x => x.CareerYearStart)
                .InclusiveBetween(1950, DateTime.UtcNow.Year);

            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
