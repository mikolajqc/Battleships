using Battleships.Domain;
using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.Factories;
using Battleships.Services.Abstraction.IO;

namespace Battleships.Services;

public class GameService : IGameService
{
    private readonly IStateOutput _stateOutput;
    private readonly IMovesInput _movesInput;
    private readonly ITextOutput _textOutput;
    private readonly IGameState _state;

    public GameService(
        IStateOutput stateOutput,
        IMovesInput movesInput,
        ITextOutput textOutput,
        IGameStateFactory gameStateFactory)
    {
        _stateOutput = stateOutput;
        _movesInput = movesInput;
        _textOutput = textOutput;
        _state = gameStateFactory.Create();
    }

    public bool IsGameOver() => _state.IsGameOver();

    public void PrintState() => _stateOutput.WriteState(_state);

    public void ReadPlayerMove()
    {
        var move = _movesInput.ReadNextMove();

        if (move == null)
        {
            _textOutput.WriteLine("I can't understand your last input. Try again.");
        }
        else
        {
            if (_state.IsShotPossible(move))
            {
                _state.MakeShot(move);
            }
            else
            {
                _textOutput.WriteLine("You cannot make this shot. It is outside of the board");
            }
        }
    }
}