using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Battleships.Utils;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Domain.Models;

public class ShipInfoTests
{
    [Fact]
    public void GetAllCoordinates_ShouldReturnExpectedListOfCoordinates_WhenShipIsVertical()
    {
        // Arrange
        var shipInfo = new ShipInfo(new Coordinates(2, 2), Orientation.Vertical, 3);

        // Act
        var coordinates = shipInfo.GetAllCoordinates();

        // Assert
        coordinates.Should().BeEquivalentTo(new List<Coordinates>
        {
            new(2, 2),
            new(3, 2),
            new(4, 2)
        });
    }
    
    [Fact]
    public void GetAllCoordinates_ShouldReturnExpectedListOfCoordinates_WhenShipIsHorizontal()
    {
        // Arrange
        var shipInfo = new ShipInfo(new Coordinates(2, 2), Orientation.Horizontal, 3);

        // Act
        var coordinates = shipInfo.GetAllCoordinates();

        // Assert
        coordinates.Should().BeEquivalentTo(new List<Coordinates>
        {
            new(2, 2),
            new(2, 3),
            new(2, 4)
        });
    }
}