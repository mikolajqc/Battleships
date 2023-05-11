using Battleships.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Domain.Entities;

public class FieldTests
{
    [Fact]
    public void IsShot_ShouldReturnFalse_WhenFieldWasNotShot()
    {
        // Arrange
        var field = new Field();

        // Act
        var isShot = field.IsShot;

        // Assert
        isShot.Should().BeFalse();
    }

    [Fact]
    public void IsShot_ShouldReturnTrue_WhenFieldWasShot()
    {
        // Arrange
        var field = new Field();

        // Act
        field.MakeShot();

        // Assert
        field.IsShot.Should().BeTrue();
    }

    [Fact]
    public void Field_AddShip_SetsShip()
    {
        // Arrange
        var field = new Field();
        var ship = new Ship(new List<Field> { field });

        // Act
        field.AddShip(ship);

        // Assert
        field.Ship.Should().Be(ship);
    }
}