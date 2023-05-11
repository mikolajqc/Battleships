using Battleships.Domain;
using Battleships.Services.Abstraction.IO;
using Battleships.Services.IO;
using Moq;
using Xunit;

namespace Battleships.Tests.Services.IO;

public class StateTextOutputTests
{
    private readonly Mock<IStateSerializer> _stateSerializerMock;
    private readonly Mock<ITextOutput> _textOutputMock;
    private readonly StateTextOutput _stateTextOutput;

    public StateTextOutputTests()
    {
        _stateSerializerMock = new Mock<IStateSerializer>();
        _textOutputMock = new Mock<ITextOutput>();
        _stateTextOutput = new StateTextOutput(_stateSerializerMock.Object, _textOutputMock.Object);
    }

    [Fact]
    public void WriteState_ShouldSerializeStateAndWriteToTextOutput()
    {
        // Arrange
        var mockState = new Mock<IGameState>();
        const string serializedState = "Serialized state";

        _stateSerializerMock.Setup(s => s.Serialize(mockState.Object)).Returns(serializedState);

        // Act
        _stateTextOutput.WriteState(mockState.Object);

        // Assert
        _textOutputMock.Verify(t => t.WriteLine(serializedState), Times.Once);
    }
}