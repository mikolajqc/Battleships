using Battleships.Domain;

namespace Battleships.Services.Abstraction.IO;

public interface IStateOutput
{
    void WriteState(IGameState state);
}