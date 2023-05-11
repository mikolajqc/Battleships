using Battleships.Domain.Models;

namespace Battleships.Services.Abstraction;

public interface IShipsGenerationService
{
    IEnumerable<ShipInfo> Generate(int boardWidth, int boardHeight, IEnumerable<int> sizesOfShipsToGenerate);
}