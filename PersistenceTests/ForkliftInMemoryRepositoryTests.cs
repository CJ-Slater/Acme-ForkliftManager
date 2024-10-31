using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Entities;
using FluentAssertions;
using Persistence.Repositories;
using Xunit;

namespace PersistenceTests
{
    public class ForkliftInMemoryRepositoryTests
    {
        private readonly ForkliftInMemoryRepository _repository;
        private readonly List<Forklift> _initialForklifts;

        public ForkliftInMemoryRepositoryTests()
        {
            // Initialize the repository with some default forklifts.
            _initialForklifts = new List<Forklift>
        {
            new Forklift { Name = "Forklift1", ModelNumber = "ModelX", ManufacturingDate = DateTime.UtcNow },
            new Forklift { Name = "Forklift2", ModelNumber = "ModelY", ManufacturingDate = DateTime.UtcNow.AddYears(-1) }
        };
            _repository = new ForkliftInMemoryRepository(new List<Forklift>(_initialForklifts));
        }

        [Fact]
        public async Task CreateAsync_UniqueName_AddsForklift()
        {
            // Arrange
            var newForklift = new Forklift { Name = "Forklift3", ModelNumber = "ModelZ", ManufacturingDate = DateTime.UtcNow.AddMonths(-6) };

            // Act
            var result = await _repository.CreateAsync(newForklift);

            // Assert
            result.Should().BeTrue();
            var allForklifts = await _repository.GetAllAsync();
            allForklifts.Should().HaveCount(_initialForklifts.Count + 1);
            allForklifts.Should().Contain(f => f.Name == newForklift.Name);
        }

        [Fact]
        public async Task CreateAsync_DuplicateName_ThrowsInvalidOperationException()
        {
            // Arrange
            var duplicateForklift = new Forklift { Name = "Forklift1", ModelNumber = "ModelZ", ManufacturingDate = DateTime.UtcNow };

            // Act
            Func<Task> act = async () => await _repository.CreateAsync(duplicateForklift);

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>()
                     .WithMessage("A forklift with the name 'Forklift1' already exists.");
        }

        [Fact]
        public async Task DeleteAllAsync_RemovesAllForklifts()
        {
            // Act
            await _repository.DeleteAllAsync();
            var allForklifts = await _repository.GetAllAsync();

            // Assert
            allForklifts.Should().BeEmpty();
        }

        [Fact]
        public async Task GetAllAsync_ReturnsAllForklifts()
        {
            // Act
            var result = await _repository.GetAllAsync();

            // Assert
            result.Should().HaveCount(_initialForklifts.Count);
            result.Should().Contain(f => f.Name == "Forklift1");
            result.Should().Contain(f => f.Name == "Forklift2");
        }

        [Fact]
        public async Task GetByNameAsync_ExistingName_ReturnsCorrectForklift()
        {
            // Act
            var result = await _repository.GetByNameAsync("Forklift1");

            // Assert
            result.Should().NotBeNull();
            result.Name.Should().Be("Forklift1");
            result.ModelNumber.Should().Be("ModelX");
        }

        [Fact]
        public async Task GetByNameAsync_NonExistingName_ThrowsInvalidOperationException()
        {
            // Act
            Func<Task> act = async () => await _repository.GetByNameAsync("NonExistentForklift");

            // Assert
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task GetAsync_WithFilter_ReturnsFilteredForklifts()
        {
            // Arrange
            Expression<Func<Forklift, bool>> filter = f => f.Name == "Forklift1";

            // Act
            var result = await _repository.GetAsync(filter);

            // Assert
            result.Should().HaveCount(1);
            result.First().Name.Should().Be("Forklift1");
        }

        [Fact]
        public async Task GetAsync_NoFilter_ReturnsAllForklifts()
        {
            // Act
            var result = await _repository.GetAsync();

            // Assert
            result.Should().HaveCount(_initialForklifts.Count);
        }
    }

}