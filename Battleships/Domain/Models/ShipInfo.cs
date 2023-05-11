using Battleships.Domain.Enums;
using Battleships.Utils;

namespace Battleships.Domain.Models;

public class ShipInfo
{
    public Coordinates Position { get; }
    public Orientation Orientation { get; }
    public int Size { get; }

    public ShipInfo(Coordinates position, Orientation orientation, int size)
    {
        Position = position;
        Orientation = orientation;
        Size = size;
    }

    public IEnumerable<Coordinates> GetAllCoordinates()
    {
        for (var i = 0; i < Size; i++)
        {
            yield return Orientation switch
            {
                Orientation.Horizontal => Position.ShiftBy(verticalShift: 0, horizontalShift: i),
                Orientation.Vertical => Position.ShiftBy(verticalShift: i, horizontalShift: 0),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}