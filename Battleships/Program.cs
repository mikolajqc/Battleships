using Battleships.Services.Abstraction;
using Battleships.Services.Abstraction.IO;
using Battleships.Utils;
using Microsoft.Extensions.DependencyInjection;

var serviceProvider = ServiceProviderHelper.ConfigureServiceProvider();
var gameService = serviceProvider.GetRequiredService<IGameService>();
var textOutput = serviceProvider.GetRequiredService<ITextOutput>();

while (!gameService.IsGameOver())
{
    gameService.PrintState();
    gameService.ReadPlayerMove();
}

textOutput.WriteLine("\nYou won ! :)");