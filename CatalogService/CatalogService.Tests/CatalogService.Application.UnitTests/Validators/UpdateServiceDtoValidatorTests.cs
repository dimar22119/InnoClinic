using CatalogService.Application.Models.Services;
using CatalogService.Application.Validators;
using CatalogService.Domain.Common;
using CatalogService.Domain.ValueObjects;
using FluentValidation.TestHelper;

namespace CatalogService.Application.UnitTests.Validators
{
    public class UpdateServiceDtoValidatorTests
    {
        private readonly UpdateServiceDtoValidator _validator;

        public UpdateServiceDtoValidatorTests()
        {
            _validator = new UpdateServiceDtoValidator();
        }

        [Fact]
        public void UpdateServiceDto_Name_ShouldHaveError_WhenEmpty()
        {
            // Arrange
            var model = new UpdateServiceDto("", 10, true, ServiceCategory.Consultation);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void UpdateServiceDto_Name_ShouldHaveError_WhenTooLong()
        {
            // Arrange
            var model = new UpdateServiceDto(new string('a', FieldConstraints.ServiceNameMaxLength + 1), 10, true, ServiceCategory.Consultation);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void UpdateServiceDto_Price_ShouldHaveError_WhenZeroOrLess(decimal invalidPrice)
        {
            // Arrange
            var model = new UpdateServiceDto("Name", invalidPrice, true, ServiceCategory.Consultation);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void UpdateServiceDto_Category_ShouldHaveError_WhenInvalidEnum()
        {
            // Arrange
            var model = new UpdateServiceDto("Name", 10, true, (ServiceCategory)999);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }

        [Fact]
        public void UpdateServiceDto_ShouldNotHaveError_WhenValid()
        {
            // Arrange
            var model = new UpdateServiceDto("Valid Service", 10.0m, true, ServiceCategory.Consultation);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
