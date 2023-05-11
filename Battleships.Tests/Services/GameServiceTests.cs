using Battleships.Domain;
using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Battleships.Services;
using Battleships.Services.Abstraction.Factories;
using Battleships.Services.Abstraction.IO;
using Battleships.Utils;
using Moq;
using Xunit;

namespace Battleships.Tests.Services;

public class GameServiceTests
{
    private readonly Mock<IStateOutput> _stateOutputMock = new();
    private readonly Mock<IMovesInput> _movesInputMock = new();
    private readonly Mock<ITextOutput> _textOutputMock = new();
    private readonly Mock<IGameStateFactory> _gameStateFactoryMock = new();

    [Fact]
    public void GameService_ShouldCreateGameState_WhenConstructed()
    {
        // Arrange
        var shipInfos = new List<ShipInfo> { new(new Coordinates(0, 0), Orientation.Horizontal, 5) };
        _gameStateFactoryMock
            .Setup(s => s.Create())
            .Returns(new GameState(shipInfos, 10, 10));

        // Act
        var _ = new GameService(_stateOutputMock.Object, _movesInputMock.Object, _textOutputMock.Object,
            _gameStateFactoryMock.Object);

        // Assert
        _gameStateFactoryMock.Verify(s => s.Create(), Times.Once);
    }

    [Fact]
    public void PrintState_ShouldOutputGameState_WhenCalled()
    {
        // Arrange
        var shipInfos = new List<ShipInfo> { new(new Coordinates(0, 0), Orientation.Horizontal, 5) };
        _gameStateFactoryMock
            .Setup(s => s.Create())
            .Returns(new GameState(shipInfos, 10, 10));
        var gameService = new GameService(_stateOutputMock.Object, _movesInputMock.Object, _textOutputMock.Object,
            _gameStateFactoryMock.Object);

        // Act
        gameService.PrintState();

        // Assert
        _stateOutputMock.Verify(s => s.WriteState(It.IsAny<GameState>()), Times.Once);
    }

    [Fact]
    public void ReadPlayerMove_ShouldPrintErrorMessage_WhenMoveIsInvalid()
    {
        // Arrange
        var gameStateFactoryMock = new Mock<IGameStateFactory>();
        var gameStateMock = new Mock<IGameState>();
        gameStateMock.Setup(gs => gs.IsShotPossible(It.IsAny<Coordinates>())).Returns(false);
        gameStateFactoryMock
            .Setup(gsf => gsf.Create())
            .Returns(gameStateMock.Object);

        var textOutputMock = new Mock<ITextOutput>();
        textOutputMock.Setup(to => to.WriteLine(It.IsAny<string>()));

        var movesInputMock = new Mock<IMovesInput>();
        movesInputMock.Setup(mi => mi.ReadNextMove()).Returns(new Coordinates(0, 0));

        var service = new GameService(null!, movesInputMock.Object, textOutputMock.Object,
            gameStateFactoryMock.Object);

        // Act
        service.ReadPlayerMove();

        // Assert
        textOutputMock.Verify(to => to.WriteLine("You cannot make this shot. It is outside of the board"), Times.Once);
    }

    [Fact]
    public void ReadPlayerMove_ShouldPrintErrorMessage_WhenMoveIsNull()
    {
        // Arrange
        var gameStateFactoryMock = new Mock<IGameStateFactory>();
        var gameStateMock = new Mock<IGameState>();

        var textOutputMock = new Mock<ITextOutput>();
        textOutputMock.Setup(to => to.WriteLine(It.IsAny<string>()));

        var movesInputMock = new Mock<IMovesInput>();
        movesInputMock.Setup(mi => mi.ReadNextMove()).Returns<Coordinates>(null);

        var service = new GameService(null!, movesInputMock.Object, textOutputMock.Object,
            gameStateFactoryMock.Object);
        gameStateFactoryMock
            .Setup(gsf => gsf.Create())
            .Returns(gameStateMock.Object);

        // Act
        service.ReadPlayerMove();

        // Assert
        textOutputMock.Verify(to => to.WriteLine("I can't understand your last input. Try again."), Times.Once);
    }

    [Fact]
    public void ReadPlayerMove_ShouldMakeShot_WhenMoveIsValid()
    {
        // Arrange
        var gameStateFactoryMock = new Mock<IGameStateFactory>();
        var gameStateMock = new Mock<IGameState>();
        gameStateMock.Setup(gs => gs.IsShotPossible(It.IsAny<Coordinates>())).Returns(true);
        gameStateMock.Setup(gs => gs.MakeShot(It.IsAny<Coordinates>()));

        gameStateFactoryMock
            .Setup(gsf => gsf.Create())
            .Returns(gameStateMock.Object);

        var movesInputMock = new Mock<IMovesInput>();
        movesInputMock.Setup(mi => mi.ReadNextMove()).Returns(new Coordinates(0, 0));

        var service = new GameService(null!, movesInputMock.Object, null!, gameStateFactoryMock.Object);

        // Act
        service.ReadPlayerMove();

        // Assert
        gameStateMock.Verify(gs => gs.MakeShot(It.IsAny<Coordinates>()), Times.Once);
    }
}