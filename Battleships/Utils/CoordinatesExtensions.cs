namespace Battleships.Utils;

public static class CoordinatesExtensions
{
    public static Coordinates ShiftBy(this Coordinates coordinates, int verticalShift, int horizontalShift) =>
        new(coordinates.Row + verticalShift, coordinates.Column + horizontalShift);
}