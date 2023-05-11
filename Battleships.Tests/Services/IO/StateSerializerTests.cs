using Battleships.Domain;
using Battleships.Domain.Entities;
using Battleships.Services.IO;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services.IO;

public class StateSerializerTests
{
    private readonly StateSerializer _stateSerializer;

    public StateSerializerTests()
    {
        _stateSerializer = new StateSerializer();
    }

    [Fact]
    public void Serialize_ShouldReturnCorrectRepresentation_WhenFieldsAreEmpty()
    {
        // Arrange
        var mockState = new Mock<IGameState>();
        var fields = Get2X2Fields();
        mockState.Setup(s => s.Fields).Returns(fields);
        
        // Act
        var result = _stateSerializer.Serialize(mockState.Object);

        // Assert
        result.Should().Be($"  A B{Environment.NewLine}1 . . {Environment.NewLine}2 . . {Environment.NewLine}");
    }

    [Fact]
    public void Serialize_ShouldReturnCorrectRepresentation_WhenExistsAtLeastOneShotFieldWithoutShip()
    {
        // Arrange
        var mockState = new Mock<IGameState>();
        var fields = Get2X2Fields();

        fields[0, 0].MakeShot();
        fields[1, 0].MakeShot();

        mockState.Setup(s => s.Fields).Returns(fields);

        // Act
        var result = _stateSerializer.Serialize(mockState.Object);

        // Assert
        result.Should().Be($"  A B{Environment.NewLine}1 M M {Environment.NewLine}2 . . {Environment.NewLine}");
    }

    [Fact]
    public void Serialize_ShouldReturnCorrectRepresentation_WhenExistsAtLeastOneShotFieldWithShip()
    {
        // Arrange
        var mockState = new Mock<IGameState>();
        var fields = Get2X2Fields();
        var ship = new Ship(new List<Field> { new() });

        fields[0, 0].MakeShot();
        fields[0, 0].AddShip(ship);
        mockState.Setup(s => s.Fields).Returns(fields);

        // Act
        var result = _stateSerializer.Serialize(mockState.Object);

        // Assert
        result.Should().Be($"  A B{Environment.NewLine}1 H . {Environment.NewLine}2 . . {Environment.NewLine}");
    }

    [Fact]
    public void Serialize_ShouldReturnCorrectRepresentation_WhenExistsAtLeastOneSunkShip()
    {
        // Arrange
        var mockState = new Mock<IGameState>();
        var fields = Get2X2Fields();

        fields[0, 0] = new Field();
        var ship = new Ship(new List<Field> { fields[0, 0] });
        fields[0, 0].MakeShot();
        fields[0, 0].AddShip(ship);

        mockState.Setup(s => s.Fields).Returns(fields);

        // Act
        var result = _stateSerializer.Serialize(mockState.Object);

        // Assert
        result.Should().Be($"  A B{Environment.NewLine}1 S . {Environment.NewLine}2 . . {Environment.NewLine}");
    }

    private static Field[,] Get2X2Fields() =>
        new Field[2, 2]
        {
            { new(), new() },
            { new(), new() }
        };
}