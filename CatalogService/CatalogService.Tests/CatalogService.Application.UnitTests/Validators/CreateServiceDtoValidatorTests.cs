using CatalogService.Application.Models.Services;
using CatalogService.Application.Validators;
using CatalogService.Domain.Common;
using CatalogService.Domain.ValueObjects;
using FluentValidation.TestHelper;

namespace CatalogService.Application.UnitTests.Validators
{
    public class CreateServiceDtoValidatorTests
    {
        private readonly CreateServiceDtoValidator _createServiceValidator;

        public CreateServiceDtoValidatorTests()
        {
            _createServiceValidator = new CreateServiceDtoValidator();
        }

        [Fact]
        public void CreateServiceDto_Name_ShouldHaveError_WhenEmpty()
        {
            // Arrange
            var model = new CreateServiceDto(Guid.NewGuid(), "", 10, true, ServiceCategory.Consultation);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateServiceDto_Name_ShouldHaveError_WhenTooLong()
        {
            // Arrange
            var model = new CreateServiceDto(Guid.NewGuid(), new string('a', FieldConstraints.SpecializationNameMaxLength + 1), 10, true, ServiceCategory.Consultation);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        public void CreateServiceDto_Price_ShouldHaveError_WhenZeroOrLess(decimal invalidPrice)
        {
            // Arrange
            var model = new CreateServiceDto(Guid.NewGuid(), "Name", invalidPrice, true, ServiceCategory.Consultation);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Price);
        }

        [Fact]
        public void CreateServiceDto_SpecializationId_ShouldHaveError_WhenEmpty()
        {
            // Arrange
            var model = new CreateServiceDto(Guid.Empty, "Name", 10, true, ServiceCategory.Consultation);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.SpecializationId);
        }

        [Fact]
        public void CreateServiceDto_ShouldNotHaveError_WhenValid()
        {
            // Arrange
            var model = new CreateServiceDto(Guid.NewGuid(), "Valid Service", 10.0m, true, ServiceCategory.Consultation);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }

        [Fact]
        public void CreateServiceDto_Category_ShouldHaveError_WhenInvalidEnum()
        {
            // Arrange
            var model = new CreateServiceDto(Guid.NewGuid(), "Name", 10, true, (ServiceCategory)999);

            // Act
            var result = _createServiceValidator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Category);
        }
    }
}
