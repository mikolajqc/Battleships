namespace Battleships.Services.Abstraction;

public interface IGameService
{
    bool IsGameOver();
    void PrintState();
    void ReadPlayerMove();
}