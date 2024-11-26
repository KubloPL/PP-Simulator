using Simulator.Maps;

public abstract class SmallMap : Map
{
    protected SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        ValidateSize(sizeX, nameof(SizeX), "Too wide", 20);
        ValidateSize(sizeY, nameof(SizeY), "Too long", 20);
    }

    private static void ValidateSize(int value, string parameterName, string message, int maxAllowed)
    {
        if (value > maxAllowed)
            throw new ArgumentOutOfRangeException(parameterName, message);
    }
}