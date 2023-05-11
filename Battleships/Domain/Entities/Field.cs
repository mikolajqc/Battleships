namespace Battleships.Domain.Entities;

public class Field
{
    public bool IsShot { get; private set; }
    public Ship? Ship { get; private set; }

    public Field()
    {
        IsShot = false;
    }

    public void MakeShot() => IsShot = true;
    public void AddShip(Ship ship) => Ship = ship;
}