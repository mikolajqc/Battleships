using Battleships.Domain.Enums;
using Battleships.Domain.Models;
using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.Utils;
using Battleships.Utils;

namespace Battleships.Services;

public class ShipsGenerationService : IShipsGenerationService
{
    private readonly IRandomGenerator _random;

    public ShipsGenerationService(IRandomGenerator random)
    {
        _random = random;
    }

    public IEnumerable<ShipInfo> Generate(int boardWidth, int boardHeight, IEnumerable<int> sizesOfShipsToGenerate)
    {
        var ships = new List<ShipInfo>();
        const int maxLoopCount = 100;

        foreach (var size in sizesOfShipsToGenerate)
        {
            for (var i = 0; i < maxLoopCount; ++i)
            {
                var ship = GetShip(boardWidth, boardHeight, size);
                var coordinatesOfShip = ship.GetAllCoordinates().ToList();

                if (!IsInsideTheBoard(coordinatesOfShip, boardWidth, boardHeight) ||
                    IsOverlap(coordinatesOfShip, ships)) continue;

                ships.Add(ship);
                break;
            }
        }

        return ships;
    }

    private static bool IsInsideTheBoard(IEnumerable<Coordinates> coordinatesOfShip, int boardWidth, int boardHeight) =>
        coordinatesOfShip.All(
            c => c.Row >= 0 &&
                 c.Row < boardHeight &&
                 c.Column >= 0 &&
                 c.Column < boardWidth);

    private static bool IsOverlap(IReadOnlyCollection<Coordinates> coordinatesOfShip, List<ShipInfo> ships)
    {
        foreach (var shipInfo in ships)
        {
            if (coordinatesOfShip.Any(c => shipInfo.GetAllCoordinates().Contains(c)))
            {
                return true;
            }
        }

        return false;
    }

    private ShipInfo GetShip(int boardWidth, int boardHeight, int size) =>
        new(
            new Coordinates(_random.Next(0, boardHeight),
                _random.Next(0, boardWidth)), (Orientation)_random.Next(1, 3), size);
}