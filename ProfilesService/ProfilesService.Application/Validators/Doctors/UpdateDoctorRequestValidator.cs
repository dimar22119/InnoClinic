using FluentValidation;
using ProfilesService.Application.Dtos.Doctors;

namespace ProfilesService.Application.Validators.Doctors
{
    public class UpdateDoctorRequestValidator : AbstractValidator<UpdateDoctorRequest>
    {
        public UpdateDoctorRequestValidator()
        {
            RuleFor(x => x.Id).NotEmpty();

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First name is required.")
                .MaximumLength(50);

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last name is required.")
                .MaximumLength(50);

            RuleFor(x => x.CareerYearStart)
                .InclusiveBetween(1950, DateTime.UtcNow.Year);

            RuleFor(x => x.Status).IsInEnum();
        }
    }
}
