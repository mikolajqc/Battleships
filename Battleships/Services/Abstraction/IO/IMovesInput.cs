using Battleships.Utils;

namespace Battleships.Services.Abstraction.IO;

public interface IMovesInput
{
    public Coordinates? ReadNextMove();
}