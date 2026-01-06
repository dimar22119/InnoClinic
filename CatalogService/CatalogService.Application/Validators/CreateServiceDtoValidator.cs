using CatalogService.Application.Dtos.Services;
using CatalogService.Domain.Common;
using FluentValidation;

namespace CatalogService.Application.Validators
{
    public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
    {
        public CreateServiceDtoValidator() {

            RuleFor(x => x.SpecializationId)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Service name is required.")
                .MaximumLength(FieldConstraints.ServiceNameMaxLength)
                .WithMessage("Service name must not exceed 100 characters.");
      
            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Category).IsInEnum();
        }
    }
}
