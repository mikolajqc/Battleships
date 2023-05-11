using Battleships.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Domain.Entities;

public class ShipTests
{
    [Fact]
    public void IsSunk_ShouldReturnFalse_WhenNotAllFieldsAreShot()
    {
        // Arrange
        var field1 = new Field();
        var field2 = new Field();
        field2.MakeShot();
        var fields = new List<Field> { field1, field2 };
        var ship = new Ship(fields);

        // Act
        var isSunk = ship.IsSunk();

        // Assert
        isSunk.Should().BeFalse();
    }

    [Fact]
    public void IsSunk_ShouldReturnTrue_WhenAllFieldsAreShot()
    {
        // Arrange
        var field1 = new Field();
        var field2 = new Field();
        field1.MakeShot();
        field2.MakeShot();
        var fields = new List<Field> { field1, field2 };
        var ship = new Ship(fields);

        // Act
        var isSunk = ship.IsSunk();

        // Assert
        isSunk.Should().BeTrue();
    }
}