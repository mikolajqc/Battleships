using Battleships.Services.Abstraction.IO;

namespace Battleships.Services.IO;

public class ConsoleTextInput : ITextInput
{
    public string? ReadLine() => Console.ReadLine();
}