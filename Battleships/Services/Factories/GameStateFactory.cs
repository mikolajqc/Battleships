using Battleships.Domain;
using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.Factories;

namespace Battleships.Services.Factories;

public class GameStateFactory : IGameStateFactory
{
    private const int BoardWidth = 10;
    private const int BoardHeight = 10;
    private static readonly int[] SizesOfShipsToGenerate = { 5, 4, 4 };

    private readonly IShipsGenerationService _shipsGenerationService;

    public GameStateFactory(IShipsGenerationService shipsGenerationService)
    {
        _shipsGenerationService = shipsGenerationService;
    }

    public IGameState Create()
    {
        return new GameState(_shipsGenerationService.Generate(BoardWidth, BoardHeight, SizesOfShipsToGenerate).ToList(),
            BoardWidth,
            BoardHeight);
    }
}