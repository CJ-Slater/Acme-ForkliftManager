using MediatR;
using Moq;
using Xunit;
using FluentAssertions;
using Application.Requests.Forklift;
using Domain.Enums;
using Infrastructure.Services.ForkliftServices;

namespace InfrastructureTests
{


    public class ForkliftStringCommandServiceTests
    {
        private readonly Mock<ISender> _mediatorMock;
        private readonly ForkliftStringCommandService _service;

        public ForkliftStringCommandServiceTests()
        {
            _mediatorMock = new Mock<ISender>();
            _service = new ForkliftStringCommandService(_mediatorMock.Object);
        }

        [Fact]
        public async Task ParseMovementAsync_ValidCommands_ReturnsExpectedResponse()
        {
            // Arrange
            var commandString = "F10L90R90B5";
            var forkliftName = "Forklift1";
            var expectedRequest = new BatchMoveForkliftRequest
            {
                ForkliftName = forkliftName,
                Commands = new List<MoveForkliftRequest>
            {
                new MoveForkliftRequest { Distance = 10 },
                new MoveForkliftRequest { RotationDirection = RotationDirection.Left, Degrees = 90 },
                new MoveForkliftRequest { RotationDirection = RotationDirection.Right, Degrees = 90 },
                new MoveForkliftRequest { Distance = -5 }
            }
            };
            var expectedResponse = new BatchMoveForkliftResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<BatchMoveForkliftRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.ParseMovementAsync(commandString, forkliftName);

            // Assert
            result.Should().Be(expectedResponse);
            _mediatorMock.Verify(m => m.Send(It.Is<BatchMoveForkliftRequest>(req =>
                req.ForkliftName == forkliftName &&
                req.Commands.Count == expectedRequest.Commands.Count &&
                req.Commands[0].Distance == expectedRequest.Commands[0].Distance &&
                req.Commands[1].RotationDirection == expectedRequest.Commands[1].RotationDirection &&
                req.Commands[2].Degrees == expectedRequest.Commands[2].Degrees &&
                req.Commands[3].Distance == expectedRequest.Commands[3].Distance
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ParseMovementAsync_InvalidCommand_ThrowsInvalidDataException()
        {
            // Arrange
            var invalidCommand = "X10";
            var forkliftName = "Forklift1";

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => _service.ParseMovementAsync(invalidCommand, forkliftName));
        }

        [Fact]
        public async Task ParseMovementAsync_InvalidNumber_ThrowsInvalidDataException()
        {
            // Arrange
            var invalidCommand = "F-10";
            var forkliftName = "Forklift1";

            // Act & Assert
            await Assert.ThrowsAsync<InvalidDataException>(() => _service.ParseMovementAsync(invalidCommand, forkliftName));
        }

        [Fact]
        public async Task ParseMovementAsync_ValidRotationCommands_ReturnsExpectedResponse()
        {
            // Arrange
            var commandString = "L180R90";
            var forkliftName = "Forklift2";
            var expectedRequest = new BatchMoveForkliftRequest
            {
                ForkliftName = forkliftName,
                Commands = new List<MoveForkliftRequest>
            {
                new MoveForkliftRequest { RotationDirection = RotationDirection.Left, Degrees = 180 },
                new MoveForkliftRequest { RotationDirection = RotationDirection.Right, Degrees = 90 }
            }
            };
            var expectedResponse = new BatchMoveForkliftResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<BatchMoveForkliftRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.ParseMovementAsync(commandString, forkliftName);

            // Assert
            result.Should().Be(expectedResponse);
            _mediatorMock.Verify(m => m.Send(It.Is<BatchMoveForkliftRequest>(req =>
                req.ForkliftName == forkliftName &&
                req.Commands.Count == expectedRequest.Commands.Count &&
                req.Commands[0].RotationDirection == expectedRequest.Commands[0].RotationDirection &&
                req.Commands[0].Degrees == expectedRequest.Commands[0].Degrees &&
                req.Commands[1].RotationDirection == expectedRequest.Commands[1].RotationDirection &&
                req.Commands[1].Degrees == expectedRequest.Commands[1].Degrees
            ), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task ParseMovementAsync_EmptyCommandString_ReturnsEmptyResponse()
        {
            // Arrange
            var commandString = "";
            var forkliftName = "Forklift1";
            var expectedRequest = new BatchMoveForkliftRequest
            {
                ForkliftName = forkliftName,
                Commands = new List<MoveForkliftRequest>()
            };
            var expectedResponse = new BatchMoveForkliftResponse();
            _mediatorMock.Setup(m => m.Send(It.IsAny<BatchMoveForkliftRequest>(), It.IsAny<CancellationToken>()))
                         .ReturnsAsync(expectedResponse);

            // Act
            var result = await _service.ParseMovementAsync(commandString, forkliftName);

            // Assert
            result.Should().Be(expectedResponse);
            _mediatorMock.Verify(m => m.Send(It.Is<BatchMoveForkliftRequest>(req =>
                req.ForkliftName == forkliftName &&
                req.Commands.Count == expectedRequest.Commands.Count
            ), It.IsAny<CancellationToken>()), Times.Once);
        }
    }

}
