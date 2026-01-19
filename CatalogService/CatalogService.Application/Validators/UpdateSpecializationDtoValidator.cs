using CatalogService.Application.Models.Specializations;
using CatalogService.Domain.Common;
using FluentValidation;

namespace CatalogService.Application.Validators
{
    public class UpdateSpecializationDtoValidator : AbstractValidator<UpdateSpecializationDto>
    {
        public UpdateSpecializationDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Service name is required.")
                .MaximumLength(FieldConstraints.SpecializationNameMaxLength)
                .WithMessage("Specialization name must not exceed 100 characters.");

            RuleFor(x => x.IsActive)
                .NotNull()
                .WithMessage("IsActive status is required.");
        }
    }
}
