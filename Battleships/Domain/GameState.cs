using Battleships.Domain.Entities;
using Battleships.Domain.Models;
using Battleships.Utils;

namespace Battleships.Domain;

public class GameState : IGameState
{
    public Field[,] Fields { get; }
    public List<Ship> Ships { get; }

    public GameState(IReadOnlyCollection<ShipInfo> ships, int width, int height)
    {
        if (!AreShipsValid(ships, width, height))
        {
            throw new ArgumentException("Provided ships are invalid. They either overlap or are out of board bounds.");
        }

        Fields = new Field[height, width];
        Ships = new List<Ship>();

        InitializeFields(height, width);
        InitializeShips(ships);
    }

    public void MakeShot(Coordinates coordinates)
    {
        Fields[coordinates.Column, coordinates.Row].MakeShot();
    }

    public bool IsShotPossible(Coordinates coordinates)
    {
        var width = Fields.GetLength(0);
        var height = Fields.GetLength(1);

        return coordinates.Column < width && coordinates.Row < height && coordinates is { Column: >= 0, Row: >= 0 };
    }

    public bool IsGameOver() => Ships.All(s => s.IsSunk());

    private static bool AreShipsValid(IEnumerable<ShipInfo> shipInfos, int width, int height)
    {
        var shipCoordinates = new HashSet<Coordinates>();

        foreach (var shipInfo in shipInfos)
        {
            var allCoordinatesOfShip = shipInfo.GetAllCoordinates().ToList();

            if (allCoordinatesOfShip.Any(coordinate =>
                    coordinate.Row < 0 || coordinate.Column < 0 ||
                    coordinate.Row >= height || coordinate.Column >= width))
            {
                return false;
            }

            if (allCoordinatesOfShip.Any(coordinate => !shipCoordinates.Add(coordinate)))
            {
                return false;
            }
        }

        return true;
    }

    private void InitializeShips(IEnumerable<ShipInfo> shipInfos)
    {
        foreach (var shipInfo in shipInfos)
        {
            var allCoordinatesOfShip = shipInfo.GetAllCoordinates();
            var fieldsUnderTheShip = GetFieldsByCoordinates(allCoordinatesOfShip).ToList();
            var ship = new Ship(fieldsUnderTheShip);

            foreach (var field in fieldsUnderTheShip)
            {
                field.AddShip(ship);
            }

            Ships.Add(ship);
        }
    }

    private void InitializeFields(int height, int width)
    {
        for (var row = 0; row < height; row++)
        {
            for (var column = 0; column < width; column++)
            {
                Fields[column, row] = new Field();
            }
        }
    }

    private IEnumerable<Field> GetFieldsByCoordinates(IEnumerable<Coordinates> allCoordinatesOfShip) =>
        allCoordinatesOfShip.Select(coordinates => Fields[coordinates.Column, coordinates.Row]);
}