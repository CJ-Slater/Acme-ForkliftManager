using MediatR;
using Moq;
using Xunit;
using FluentAssertions;
using Application.Requests.Forklift;
using Domain.Enums;
using Infrastructure.Services.ForkliftServices;
using CsvHelper.Configuration;
using Infrastructure.Services.File;
using Application.File;



namespace InfrastructureTests
{


    public class ForkliftFileProcessingServiceTests
    {
        private readonly Mock<IFileValidator> _fileValidatorMock;
        private readonly CsvConfiguration _csvConfig;
        private readonly ForkliftFileProcessingService _service;

        public ForkliftFileProcessingServiceTests()
        {
            _fileValidatorMock = new Mock<IFileValidator>();
            _csvConfig = new CsvConfiguration(System.Globalization.CultureInfo.InvariantCulture);
            _service = new ForkliftFileProcessingService(_csvConfig, _fileValidatorMock.Object);
        }

        [Fact]
        public async Task ProcessFileAsync_NullFileInput_ThrowsArgumentException()
        {
            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ProcessFileAsync(null));
        }

        [Fact]
        public async Task ProcessFileAsync_EmptyFile_ThrowsArgumentException()
        {
            // Arrange
            var emptyFile = new Mock<IFileUpload>();
            emptyFile.Setup(f => f.OpenReadStream()).Returns(new MemoryStream());

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => _service.ProcessFileAsync(emptyFile.Object));
        }

        [Fact]
        public async Task ProcessFileAsync_InvalidFileFormat_ThrowsInvalidOperationException()
        {
            // Arrange
            var file = new Mock<IFileUpload>();
            file.Setup(f => f.OpenReadStream()).Returns(new MemoryStream(new byte[] { 1, 2, 3 }));
            _fileValidatorMock.Setup(v => v.ValidateFileFormat(file.Object, It.IsAny<string[]>())).Returns(false);

            // Act & Assert
            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.ProcessFileAsync(file.Object));
        }

        [Fact]
        public async Task ProcessFileAsync_ValidJsonFile_ReturnsParsedForklifts()
        {
            // Arrange
            var file = new Mock<IFileUpload>();
            var jsonContent = "[{\"Name\":\"Forklift1\",\"ModelNumber\":\"Big Model\",\"ManufacturingDate\":\"2024-01-01\"}," +
                              "{\"Name\":\"Forklift2\",\"ModelNumber\":\"Small Model\",\"ManufacturingDate\":\"2023-01-01\"}]";
            var memoryStream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(jsonContent));
            file.Setup(f => f.FileName).Returns("forklifts.json");
            file.Setup(f => f.OpenReadStream()).Returns(memoryStream);
            _fileValidatorMock.Setup(v => v.ValidateFileFormat(file.Object, It.IsAny<string[]>())).Returns(true);

            // Act
            var result = await _service.ProcessFileAsync(file.Object);

            // Assert
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Forklift1");
            result[1].ModelNumber.Should().Be("Small Model");
        }

    }

}