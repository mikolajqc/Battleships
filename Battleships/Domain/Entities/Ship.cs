namespace Battleships.Domain.Entities;

public class Ship
{
    public Ship(List<Field> fieldsUnderTheShip)
    {
        FieldsUnderTheShip = fieldsUnderTheShip;
    }

    private List<Field> FieldsUnderTheShip { get; }

    public bool IsSunk() => FieldsUnderTheShip.All(c => c.IsShot);
}