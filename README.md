# Battleships

This is the Battleships project, a simple implementation of the classic game in .NET 7.

## Requirements

- .NET 7.0 or later
- An appropriate IDE such as Visual Studio, Visual Studio Code, or Rider

## Setup

### Clone the repository:
```bash
git clone https://github.com/mikolajqc/Battleships.git
```

### Navigate to the project directory
```bash
cd Battleships
````


### Running the project

#### Restore the .NET packages
```bash
dotnet restore
```

#### Build the solution
```bash
dotnet build
```

#### Run the Battleships project
```bash
dotnet run --project Battleships/Battleships.csproj
```

### Testing

#### Run the tests
```bash
dotnet test Battleships.Tests/Battleships.Tests.csproj
```

### How to play
After running the game, you will see the board:

```bash
   A B C D E F G H I J
 1 . . . . . . . . . .
 2 . . . . . . . . . .
 3 . . . . . . . . . .
 4 . . . . . . . . . .
 5 . . . . . . . . . .
 6 . . . . . . . . . .
 7 . . . . . . . . . .
 8 . . . . . . . . . .
 9 . . . . . . . . . .
10 . . . . . . . . . .

```
You can choose the field you want to shoot at by typing its coordinates, for example: `B2`.

Field can be displayed as:

`.` - empty field

`H` - hit

`M` - missed

`S` - sunk

For example:

```bash
   A B C D E F G H I J
 1 . S . . . . . . . .
 2 . S M . . . . . . .
 3 . S . . . . . . . .
 4 . S . . . . . . . .
 5 . S . . H . . . . .
 6 . M . . . . . . . .
 7 . M . . . . . . . .
 8 . . . . . . . . . .
 9 . . . . . . . . . .
10 . . . . . . . . . .

```

In this example, you can see that the ship spanning from `B1` to `B5` has been sunk `S`, a shot at `F5` has hit but the ship hasn't been sunk yet `H`, and shots at `F6` and `F7` have missed `M`.

If you manage to sink all the ships, you will see the message: `You won! :)`

Enjoy playing Battleships!
