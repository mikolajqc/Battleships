using Battleships.Utils;

namespace Battleships.Services.Abstraction.IO;

public interface IInputParser
{
    Coordinates? Parse(string? input);
}