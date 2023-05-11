using Battleships.Services.Abstraction.Utils;

namespace Battleships.Services.Utils;

public class RandomGenerator : IRandomGenerator
{
    private readonly Random _random = new();

    public int Next(int minValue, int maxValue) => _random.Next(minValue, maxValue);
}