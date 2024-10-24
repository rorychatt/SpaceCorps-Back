namespace SpaceCorps.Business.EntityLogic;

public class Position(float x, float y, float z)
{
    private float X { get; set; } = x;
    private float Y { get; set; } = y;
    private float Z { get; set; } = z;

    public void Set(Position newPosition)
    {
        X = newPosition.X;
        Y = newPosition.Y;
        Z = newPosition.Z;
    }

    public (float X, float Y, float Z) Get()
    {
        return (X, Y, Z);
    }
}