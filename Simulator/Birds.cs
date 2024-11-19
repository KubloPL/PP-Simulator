namespace Simulator;

internal class Birds : Animals
{
    private readonly bool _canFly = true;

    public bool CanFly
    {
        get => _canFly;
        init => _canFly = value;
    }

    public override string Info()
    {
        string flyAbility = CanFly ? "fly+" : "fly-";
        return $"{Description} ({flyAbility}) <{Size}>";
    }
}