using Battleships.Services.Abstraction.IO;
using Battleships.Utils;

namespace Battleships.Services.IO;

public class MovesTextInput : IMovesInput
{
    private readonly ITextInput _textInput;
    private readonly IInputParser _inputParser;

    public MovesTextInput(IInputParser inputParser, ITextInput textInput)
    {
        _inputParser = inputParser;
        _textInput = textInput;
    }

    public Coordinates? ReadNextMove()
    {
        var input = _textInput.ReadLine();
        return _inputParser.Parse(input);
    }
}