using Battleships.Domain;

namespace Battleships.Services.Abstraction.IO;

public interface IStateSerializer
{
    string Serialize(IGameState gameState);
}