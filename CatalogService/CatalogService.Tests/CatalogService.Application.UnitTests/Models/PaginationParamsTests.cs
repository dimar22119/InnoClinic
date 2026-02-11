using CatalogService.Application.Models;
using FluentAssertions;

namespace CatalogService.Application.UnitTests.Models
{
    public class PaginationParamsTests
    {
        [Theory]
        [InlineData(0, 1, 10, 10)]
        [InlineData(2, 2, 150, 100)]
        [InlineData(1, 1, -5, 10)]
        [InlineData(3, 3, 50, 50)]
        public void PaginationParams_ShouldSanitizeValues(
            int inputPage, int expectedPage,
            int inputSize, int expectedSize)
        {
            // Act
            var p = new PaginationParams(inputPage, inputSize);

            // Assert
            p.PageNumber.Should().Be(expectedPage);
            p.PageSize.Should().Be(expectedSize);
        }
    }
}
