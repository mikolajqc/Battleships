using Battleships.Services.Abstraction.IO;

namespace Battleships.Services.IO;

public class ConsoleTextOutput : ITextOutput
{
    public void WriteLine(string text)
    {
        Console.WriteLine(text);
    }
}