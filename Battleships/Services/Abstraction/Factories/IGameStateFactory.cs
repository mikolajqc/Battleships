using Battleships.Domain;

namespace Battleships.Services.Abstraction.Factories;

public interface IGameStateFactory
{
    IGameState Create();
}