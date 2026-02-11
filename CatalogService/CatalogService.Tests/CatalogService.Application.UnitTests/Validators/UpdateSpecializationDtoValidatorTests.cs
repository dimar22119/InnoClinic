using CatalogService.Application.Models.Specializations;
using CatalogService.Application.Validators;
using CatalogService.Domain.Common;
using FluentValidation.TestHelper;

namespace CatalogService.Application.UnitTests.Validators
{
    public class UpdateSpecializationDtoValidatorTests
    {
        private readonly UpdateSpecializationDtoValidator _validator;

        public UpdateSpecializationDtoValidatorTests()
        {
            _validator = new UpdateSpecializationDtoValidator();
        }

        [Fact]
        public void UpdateSpecializationDto_Name_ShouldHaveError_WhenEmpty()
        {
            // Arrange
            var model = new UpdateSpecializationDto("", true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void UpdateSpecializationDto_Name_ShouldHaveError_WhenTooLong()
        {
            // Arrange
            var model = new UpdateSpecializationDto(new string('a', FieldConstraints.SpecializationNameMaxLength + 1), true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void UpdateSpecializationDto_ShouldNotHaveError_WhenValid()
        {
            // Arrange
            var model = new UpdateSpecializationDto("Valid Specialization", true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
