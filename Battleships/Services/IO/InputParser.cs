using System.Text.RegularExpressions;
using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.IO;
using Battleships.Utils;

namespace Battleships.Services.IO;

public class InputParser : IInputParser
{
    public Coordinates? Parse(string? input)
    {
        const string pattern = @"^[A-Z]\d+$";

        if (string.IsNullOrWhiteSpace(input))
        {
            return null;
        }

        if (!Regex.IsMatch(input, pattern))
        {
            return null;
        }

        var column = input[0] - 65;
        var row = int.Parse(input[1..]) - 1;

        return new Coordinates(row, column);
    }
}