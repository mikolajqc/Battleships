using Battleships.Domain;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Battleships.Utils;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Domain;

public class GameStateTests
{
    [Fact]
    public void GameStateConstructor_ShouldThrowArgumentException_WhenAtLeastOneShipHasCoordinatesOutOfTheBoard()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(9, 9), Orientation.Horizontal, 5),
            new(new Coordinates(0, 0), Orientation.Horizontal, 4),
            new(new Coordinates(1, 1), Orientation.Horizontal, 4),
        };

        // Act
        Action act = () => new GameState(ships, 10, 10);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GameStateConstructor_ShouldThrowArgumentException_WhenAtTwoShipsOverlap()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 5),
            new(new Coordinates(0, 0), Orientation.Horizontal, 4),
            new(new Coordinates(1, 1), Orientation.Horizontal, 4),
        };

        // Act
        Action act = () => new GameState(ships, 10, 10);

        // Assert
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void GameStateConstructor_ShouldNotThrow_WhenListOfShipsIsCorrect()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 5),
            new(new Coordinates(1, 0), Orientation.Horizontal, 4),
            new(new Coordinates(2, 0), Orientation.Horizontal, 4),
        };

        // Act
        Action act = () => new GameState(ships, 10, 10);

        // Assert
        act.Should().NotThrow<ArgumentException>();
    }

    [Fact]
    public void MakeShot_ShouldMarkFieldAsShot_WhenCoordinatesAreValid()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 5),
        };

        var gameState = new GameState(ships, 10, 10);
        var targetCoordinates = new Coordinates(0, 0);

        // Act
        gameState.MakeShot(targetCoordinates);

        // Assert
        gameState.Fields[0, 0].IsShot.Should().BeTrue();
    }

    [Fact]
    public void IsShotPossible_ShouldReturnFalse_WhenCoordinatesAreInvalid()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 5),
        };

        var gameState = new GameState(ships, 10, 10);
        var targetCoordinates = new Coordinates(-1, -1);

        // Act
        var result = gameState.IsShotPossible(targetCoordinates);

        // Assert
        result.Should().BeFalse();
    }
    
    [Fact]
    public void IsShotPossible_ShouldReturnTrue_WhenCoordinatesAreValid()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 5),
        };

        var gameState = new GameState(ships, 10, 10);
        var targetCoordinates = new Coordinates(3, 2);

        // Act
        var result = gameState.IsShotPossible(targetCoordinates);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void IsGameOver_ShouldReturnTrue_WhenAllShipsSunk()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new ShipInfo(new Coordinates(0, 0), Orientation.Horizontal, 1),
        };

        var gameState = new GameState(ships, 10, 10);
        gameState.MakeShot(new Coordinates(0, 0));

        // Act
        var result = gameState.IsGameOver();

        // Assert
        result.Should().BeTrue();
    }
    
    [Fact]
    public void IsGameOver_ShouldReturnFalse_WhenNotAllShipsSunk()
    {
        // Arrange
        var ships = new List<ShipInfo>
        {
            new(new Coordinates(0, 0), Orientation.Horizontal, 1),
            new(new Coordinates(3, 2), Orientation.Horizontal, 5),
        };

        var gameState = new GameState(ships, 10, 10);
        gameState.MakeShot(new Coordinates(0, 0));

        // Act
        var result = gameState.IsGameOver();

        // Assert
        result.Should().BeFalse();
    }
}