using Battleships.Domain;
using Battleships.Services.Abstraction.IO;

namespace Battleships.Services.IO;

public class StateTextOutput : IStateOutput
{
    private readonly IStateSerializer _stateSerializer;
    private readonly ITextOutput _textOutput;

    public StateTextOutput(IStateSerializer stateSerializer, ITextOutput textOutput)
    {
        _stateSerializer = stateSerializer;
        _textOutput = textOutput;
    }

    public void WriteState(IGameState state)
    {
        var serializedState = _stateSerializer.Serialize(state);
        _textOutput.WriteLine(serializedState);
    }
}