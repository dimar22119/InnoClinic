using CatalogService.Application.Exceptions;
using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Models;
using CatalogService.Application.Models.Services;
using CatalogService.Application.Services;
using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using CatalogService.Domain.ValueObjects;
using FluentAssertions;
using Moq;

namespace CatalogService.Application.UnitTests.Services
{
    public class ServiceManagerTests
    {
        private readonly Mock<IServiceRepository> _serviceRepoMock;
        private readonly Mock<ISpecializationRepository> _specRepoMock;
        private readonly ServiceManager _serviceManager;

        public ServiceManagerTests()
        {
            _serviceRepoMock = new Mock<IServiceRepository>();
            _specRepoMock = new Mock<ISpecializationRepository>();
            _serviceManager = new ServiceManager(_serviceRepoMock.Object, _specRepoMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnDto_WhenServiceExists()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            var service = new Service
            {
                Id = serviceId,
                Name = "Test Service",
                Price = 100,
                IsActive = true,
                Category = ServiceCategory.Consultation
            };
            _serviceRepoMock.Setup(x => x.GetByIdAsync(serviceId))
                .ReturnsAsync(service);

            // Act
            var result = await _serviceManager.GetByIdAsync(serviceId);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(serviceId);
            result.Name.Should().Be(service.Name);
            result.Price.Should().Be(service.Price);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowNotFoundException_WhenServiceDoesNotExist()
        {
            // Arrange
            var serviceId = Guid.NewGuid();
            _serviceRepoMock.Setup(x => x.GetByIdAsync(serviceId))
                .ReturnsAsync((Service?)null);

            // Act
            var act = async () => await _serviceManager.GetByIdAsync(serviceId);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service with ID {serviceId} was not found.");
        }

        [Fact]
        public async Task CreateAsync_ShouldReturnDto_WhenSpecializationExists()
        {
            // Arrange
            var specId = Guid.NewGuid();
            var dto = new CreateServiceDto(specId, "New Service", 150, true, ServiceCategory.Consultation);

            var specialization = new Specialization { Id = specId, Name = "Spec" };

            _specRepoMock.Setup(x => x.GetByIdAsync(specId))
                .ReturnsAsync(specialization);

            // Act
            var result = await _serviceManager.CreateAsync(dto);

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be(dto.Name);
            result.Price.Should().Be(dto.Price);

            _serviceRepoMock.Verify(x => x.Add(It.Is<Service>(s =>
                s.Name == dto.Name &&
                s.Price == dto.Price &&
                s.SpecializationId == dto.SpecializationId)), Times.Once);
            _serviceRepoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_ShouldThrowNotFound_WhenSpecializationDoesNotExist()
        {
            // Arrange
            var dto = new CreateServiceDto(Guid.NewGuid(), "Name", 10, true, ServiceCategory.Consultation);
            _specRepoMock.Setup(x => x.GetByIdAsync(dto.SpecializationId))
                .ReturnsAsync((Specialization?)null);

            // Act
            var act = async () => await _serviceManager.CreateAsync(dto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage("Specialization not found");
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateService_WhenServiceExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var existingService = new Service
            {
                Id = id,
                Name = "Old Name",
                SpecializationId = Guid.NewGuid()
            };

            var updateDto = new UpdateServiceDto("Updated Name", 200, false, ServiceCategory.Diagnostic);

            _serviceRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(existingService);

            // Act
            await _serviceManager.UpdateAsync(id, updateDto);

            // Assert
            _serviceRepoMock.Verify(x => x.Update(It.Is<Service>(s =>
                s.Id == id &&
                s.Name == updateDto.Name &&
                s.Price == updateDto.Price)), Times.Once);
            _serviceRepoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task UpdateAsync_ShouldThrowNotFound_WhenServiceDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            var updateDto = new UpdateServiceDto("Name", 10, true, ServiceCategory.Consultation);

            _serviceRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync((Service?)null);

            // Act
            var act = async () => await _serviceManager.UpdateAsync(id, updateDto);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service with ID {id} not found");
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteService_WhenServiceExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var service = new Service { Id = id, Name = "ServiceToDelete" };

            _serviceRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync(service);

            // Act
            await _serviceManager.DeleteAsync(id);

            // Assert
            _serviceRepoMock.Verify(x => x.Delete(service), Times.Once);
            _serviceRepoMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task DeleteAsync_ShouldThrowNotFound_WhenServiceDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceRepoMock.Setup(x => x.GetByIdAsync(id))
                .ReturnsAsync((Service?)null);

            // Act
            var act = async () => await _serviceManager.DeleteAsync(id);

            // Assert
            await act.Should().ThrowAsync<NotFoundException>()
                .WithMessage($"Service with ID {id} was not found.");
        }

        [Fact]
        public async Task GetPagedAsync_ShouldReturnPagedResponse_WhenCalled()
        {
            // Arrange
            var pagination = new PaginationParams { PageNumber = 1, PageSize = 10 };
            var service = new Service
            {
                Id = Guid.NewGuid(),
                Name = "Test Service",
                Price = 100,
                IsActive = true,
                Category = ServiceCategory.Consultation
            };
            var items = new List<Service> { service };
            var pagedList = new PagedList<Service>(items, 1);

            _serviceRepoMock.Setup(x => x.GetPagedAsync(0, 10))
                .ReturnsAsync(pagedList);

            // Act
            var result = await _serviceManager.GetPagedAsync(pagination);

            // Assert
            result.Should().NotBeNull();
            result.Data.Should().HaveCount(1);
            result.Data[0].Id.Should().Be(service.Id);
            result.PageNumber.Should().Be(1);
            result.PageSize.Should().Be(10);
            result.TotalRecords.Should().Be(1);
        }
    }
}
