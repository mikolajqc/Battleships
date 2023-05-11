using Battleships.Domain.Enums;
using Battleships.Services;
using Battleships.Services.Abstraction.Utils;
using Battleships.Utils;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services;

public class TestRandomGenerator : IRandomGenerator
{
    private readonly Queue<int> _values;

    public TestRandomGenerator(IEnumerable<int> values)
    {
        _values = new Queue<int>(values);
    }

    public int Next(int minValue, int maxValue) => _values.Dequeue();
}

public class ShipsGenerationServiceTests
{
    [Fact]
    public void Generate_ShouldGenerateShips_WhenNoOverlapAndShipsWithinTheBoard()
    {
        // Arrange
        var randomGenerator = new TestRandomGenerator(new[] { 0, 0, 1, 3, 3, 1 });
        var service = new ShipsGenerationService(randomGenerator);
        var sizesOfShipsToGenerate = new[] { 1, 2 };

        // Act
        var ships = service.Generate(10, 10, sizesOfShipsToGenerate).ToList();

        // Assert
        ships.Should().HaveCount(2);
        ships[0].Size.Should().Be(1);
        ships[0].Orientation.Should().Be(Orientation.Horizontal);
        ships[0].Position.Should().BeEquivalentTo(new Coordinates(0, 0));
        ships[1].Size.Should().Be(2);
        ships[1].Orientation.Should().Be(Orientation.Horizontal);
        ships[1].Position.Should().BeEquivalentTo(new Coordinates(3, 3));
    }
    
    [Fact]
    public void Generate_ShouldGenerateShipsWithRegeneration_WhenAtLeastTwoShipsOverlap()
    {
        // Arrange
        var randomGenerator = new TestRandomGenerator(new[] { 0, 0, 1, 0, 0, 1, 1, 1, 2 });
        var service = new ShipsGenerationService(randomGenerator);
        var sizesOfShipsToGenerate = new[] { 2, 2 };

        // Act
        var ships = service.Generate(10, 10, sizesOfShipsToGenerate).ToList();

        // Assert
        ships.Should().HaveCount(2);
        ships[0].Size.Should().Be(2);
        ships[0].Orientation.Should().Be(Orientation.Horizontal);
        ships[0].Position.Should().BeEquivalentTo(new Coordinates(0, 0));
        ships[1].Size.Should().Be(2);
        ships[1].Orientation.Should().Be(Orientation.Vertical);
        ships[1].Position.Should().BeEquivalentTo(new Coordinates(1, 1));
    }

    [Fact]
    public void Generate_ShouldGenerateShipsWithRegeneration_WhenAtLeastOneShipIsOutOfTheBoard()
    {
        // Arrange
        var randomGenerator = new TestRandomGenerator(new[] { 9, 9, 2, 5, 5, 1 });
        var service = new ShipsGenerationService(randomGenerator);
        var sizesOfShipsToGenerate = new[] { 3 };

        // Act
        var ships = service.Generate(10, 10, sizesOfShipsToGenerate).ToList();

        // Assert
        ships.Should().HaveCount(1);
        ships[0].Size.Should().Be(3);
        ships[0].Orientation.Should().Be(Orientation.Horizontal);
        ships[0].Position.Should().BeEquivalentTo(new Coordinates(5, 5));
    }
}
