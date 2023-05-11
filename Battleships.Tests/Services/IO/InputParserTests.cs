using Battleships.Services.IO;
using Battleships.Utils;
using FluentAssertions;
using Xunit;

namespace Battleships.Tests.Services.IO;

public class InputParserTests
{
    [Theory]
    [InlineData("C3", 2, 2)]
    [InlineData("A12", 11, 0)]
    [InlineData("G50", 49, 6)]
    [InlineData("", 0, 0, false)]
    [InlineData(" ", 0, 0, false)]
    [InlineData(".[[;", 0, 0, false)]
    [InlineData("123", 0, 0, false)]
    [InlineData("1", 0, 0, false)]
    [InlineData("A", 0, 0, false)]
    [InlineData("AB2", 0, 0, false)]
    [InlineData("1B", 0, 0, false)]
    [InlineData("b4", 0, 0, false)]
    public void Parse_ShouldReturnExpectedCoordinatesOrNull_DependingOnTheInput(string input, int rowNumber,
        int columnNumber, bool isParsed = true)
    {
        // Arrange
        var parser = new InputParser();

        // Act
        var result = parser.Parse(input);

        // Assert
        result.Should().BeEquivalentTo(isParsed ? new Coordinates(rowNumber, columnNumber) : null);
    }
}