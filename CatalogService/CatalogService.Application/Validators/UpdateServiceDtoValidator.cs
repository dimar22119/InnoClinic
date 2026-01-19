using CatalogService.Application.Models.Services;
using CatalogService.Domain.Common;
using FluentValidation;

namespace CatalogService.Application.Validators
{
    public class UpdateServiceDtoValidator : AbstractValidator<UpdateServiceDto>
    {
        public UpdateServiceDtoValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Service name is required.")
                .MaximumLength(FieldConstraints.ServiceNameMaxLength)
                .WithMessage("Service name must not exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Price must be greater than zero.");

            RuleFor(x => x.Category)
                .IsInEnum();
        }
    }
}
