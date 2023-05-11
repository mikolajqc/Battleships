using System.Text;
using Battleships.Domain;
using Battleships.Domain.Entities;
using Battleships.Services.Abstraction.IO;

namespace Battleships.Services.IO;

public class StateSerializer : IStateSerializer
{
    public string Serialize(IGameState state)
    {
        var width = state.Fields.GetLength(0);
        var height = state.Fields.GetLength(1);

        var padding = height.ToString().Length;
        var result = new StringBuilder("".PadLeft(padding));

        for (var w = 0; w < width; w++)
        {
            result.Append($" {(char)(65 + w)}");
        }

        result.AppendLine();

        for (var h = 0; h < height; h++)
        {
            result.Append((h + 1).ToString().PadLeft(padding) + " ");

            for (var w = 0; w < width; w++)
            {
                result.Append($"{GetStringRepresentationOfField(state.Fields[w, h])} ");
            }

            result.AppendLine();
        }

        return result.ToString();
    }

    private static string GetStringRepresentationOfField(Field field)
    {
        if (field.IsShot == false)
        {
            return ".";
        }

        if (field.Ship != null)
        {
            return field.Ship.IsSunk() ? "S" : "H";
        }

        return "M";
    }
}