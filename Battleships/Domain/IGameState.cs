using Battleships.Domain.Entities;
using Battleships.Utils;

namespace Battleships.Domain;

public interface IGameState
{
    public Field[,] Fields { get; }
    public List<Ship> Ships { get; }

    void MakeShot(Coordinates coordinates);
    bool IsShotPossible(Coordinates coordinates);
    bool IsGameOver();
}