using CatalogService.Application.Models.Specializations;
using CatalogService.Application.Validators;
using CatalogService.Domain.Common;
using FluentValidation.TestHelper;

namespace CatalogService.Application.UnitTests.Validators
{
    public class CreateSpecializationDtoValidatorTests
    {
        private readonly CreateSpecializationDtoValidator _validator;

        public CreateSpecializationDtoValidatorTests()
        {
            _validator = new CreateSpecializationDtoValidator();
        }

        [Fact]
        public void CreateSpecializationDto_Name_ShouldHaveError_WhenEmpty()
        {
            // Arrange
            var model = new CreateSpecializationDto("", true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateSpecializationDto_Name_ShouldHaveError_WhenTooLong()
        {
            // Arrange
            var model = new CreateSpecializationDto(new string('a', FieldConstraints.SpecializationNameMaxLength + 1), true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldHaveValidationErrorFor(x => x.Name);
        }

        [Fact]
        public void CreateSpecializationDto_ShouldNotHaveError_WhenValid()
        {
            // Arrange
            var model = new CreateSpecializationDto("Valid Specialization", true);

            // Act
            var result = _validator.TestValidate(model);

            // Assert
            result.ShouldNotHaveAnyValidationErrors();
        }
    }
}
