using Battleships.Services;
using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.Factories;
using Battleships.Services.Abstraction.IO;
using Battleships.Services.Abstraction.Utils;
using Battleships.Services.Factories;
using Battleships.Services.IO;
using Battleships.Services.Utils;
using Microsoft.Extensions.DependencyInjection;

namespace Battleships.Utils;

public static class ServiceProviderHelper
{
    public static ServiceProvider ConfigureServiceProvider()
    {
        var serviceCollection = new ServiceCollection();

        serviceCollection.AddTransient<IGameService, GameService>();
        serviceCollection.AddTransient<IStateOutput, StateTextOutput>();
        serviceCollection.AddTransient<IMovesInput, MovesTextInput>();
        serviceCollection.AddTransient<IInputParser, InputParser>();
        serviceCollection.AddTransient<IShipsGenerationService, ShipsGenerationService>();
        serviceCollection.AddTransient<ITextOutput, ConsoleTextOutput>();
        serviceCollection.AddTransient<ITextInput, ConsoleTextInput>();
        serviceCollection.AddTransient<IRandomGenerator, RandomGenerator>();
        serviceCollection.AddTransient<IGameStateFactory, GameStateFactory>();
        serviceCollection.AddTransient<IStateSerializer, StateSerializer>();

        return serviceCollection.BuildServiceProvider();
    }
}