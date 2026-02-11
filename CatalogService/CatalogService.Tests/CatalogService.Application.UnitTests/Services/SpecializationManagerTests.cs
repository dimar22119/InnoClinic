using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Models;
using CatalogService.Application.Models.Specializations;
using CatalogService.Application.Services;
using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using FluentAssertions;
using Moq;

namespace CatalogService.Application.UnitTests.Services
{
    public class SpecializationManagerTests
    {
        private readonly Mock<ISpecializationRepository> _repoMock;
        private readonly SpecializationManager _manager;

        public SpecializationManagerTests()
        {
            _repoMock = new Mock<ISpecializationRepository>();
            _manager = new SpecializationManager(_repoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDto_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var spec = new Specialization { Id = id, Name = "Test Spec", IsActive = true };
            _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(spec);

            // Act
            var result = await _manager.GetByIdAsync(id);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(id);
            result.Name.Should().Be("Test Spec");
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFound_WhenNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Specialization?)null);

            // Act
            var act = async () => await _manager.GetByIdAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDto_WhenValid()
        {
            // Arrange
            var dto = new CreateSpecializationDto("New Spec", true);

            // Act
            var result = await _manager.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("New Spec");

            _repoMock.Verify(x => x.Add(It.Is<Specialization>(s =>
                s.Name == dto.Name && s.IsActive == dto.IsActive)), Times.Once);
            _repoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdate_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existing = new Specialization { Id = id, Name = "Old Name" };
            var dto = new UpdateSpecializationDto("New Name", false);

            _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(existing);

            // Act
            await _manager.UpdateAsync(id, dto);

            // Assert
            _repoMock.Verify(x => x.Update(It.Is<Specialization>(s =>
                s.Id == id && s.Name == dto.Name && s.IsActive == dto.IsActive)), Times.Once);
            _repoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFound_WhenNotExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dto = new UpdateSpecializationDto("Name", true);
            _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync((Specialization?)null);

            // Act
            var act = async () => await _manager.UpdateAsync(id, dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task DeleteAsync_ShouldDelete_WhenExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var spec = new Specialization { Id = id, Name = "SpecToDelete" };
            _repoMock.Setup(x => x.GetByIdAsync(id)).ReturnsAsync(spec);

            // Act
            await _manager.DeleteAsync(id);

            // Assert
            _repoMock.Verify(x => x.Delete(spec), Times.Once);
            _repoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResponse_WhenCalled()
        {
            // Arrange
            var pagination = new PaginationParams { PageNumber = 1, PageSize = 10 };
            var spec = new Specialization { Id = Guid.NewGuid(), Name = "Spec", IsActive = true };
            var items = new List<Specialization> { spec };
            var pagedList = new PagedList<Specialization>(items, 1);

            _repoMock.Setup(x => x.GetPagedAsync(0, 10))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _manager.GetPagedAsync(pagination);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().HaveCount(1);
            result.Data[0].Id.Should().Be(spec.Id);
            result.TotalRecords.Should().Be(1);
        }

        [Theory]
        [InlineData(1, 10, 0, 10)]
        [InlineData(3, 10, 20, 10)]
        public async Task GetPagedAsync_ShouldPassCorrectSkipAndTakeToRepo(
            int page, int size, int expectedSkip, int expectedTake)
        {
            // Arrange
            var pagination = new PaginationParams(page, size);
            _repoMock.Setup(x => x.GetPagedAsync(It.IsAny<int>(), It.IsAny<int>()))
                     .ReturnsAsync(new PagedList<Specialization>([], 0));

            // Act
            await _manager.GetPagedAsync(pagination);

            // Assert
            _repoMock.Verify(x => x.GetPagedAsync(expectedSkip, expectedTake), Times.Once);
        }
    }
}
