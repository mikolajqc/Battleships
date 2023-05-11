namespace Battleships.Services.Abstraction.Utils;

public interface IRandomGenerator
{
    int Next(int minValue, int maxValue);
}