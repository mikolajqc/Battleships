using Battleships.Services.Abstraction.IO;
using Battleships.Services.IO;
using Battleships.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace Battleships.Tests.Services.IO;

public class MovesTextInputTests
{
    private readonly Mock<ITextInput> _textInputMock;
    private readonly Mock<IInputParser> _inputParserMock;
    private readonly MovesTextInput _movesTextInput;

    public MovesTextInputTests()
    {
        _textInputMock = new Mock<ITextInput>();
        _inputParserMock = new Mock<IInputParser>();
        _movesTextInput = new MovesTextInput(_inputParserMock.Object, _textInputMock.Object);
    }

    [Fact]
    public void ReadNextMove_ShouldCallTextInputReadLine_WhenCalled()
    {
        // Arrange & Act
        _movesTextInput.ReadNextMove();

        // Assert
        _textInputMock.Verify(m => m.ReadLine(), Times.Once);
    }

    [Fact]
    public void ReadNextMove_ShouldCallInputParserParse_WhenCalled()
    {
        // Arrange
        const string input = "input";
        _textInputMock.Setup(m => m.ReadLine()).Returns(input);

        // Act
        _movesTextInput.ReadNextMove();

        // Assert
        _inputParserMock.Verify(m => m.Parse(input), Times.Once);
    }

    [Fact]
    public void ReadNextMove_ShouldReturnParsedCoordinates()
    {
        // Arrange
        const string input = "input";
        var coordinates = new Coordinates(1, 1);
        _textInputMock.Setup(m => m.ReadLine()).Returns(input);
        _inputParserMock.Setup(m => m.Parse(input)).Returns(coordinates);

        // Act
        var result = _movesTextInput.ReadNextMove();

        // Assert
        result.Should().Be(coordinates);
    }
}