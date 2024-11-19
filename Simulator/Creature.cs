namespace Simulator;

public abstract class Creature
{
    private string _name = "Unknown";
    private int _level = 1;
    private bool _isNameSet = false;
    private bool _isLevelSet = false;

    public string Name
    {
        get => _name;
        set => _name = Validator.Shortener(value, 3, 25, '#');
    }

    public int Level
    {
        get => _level;
        set => _level = Validator.Limiter(value, 1, 10);
    }

    public abstract string Info { get; }
    public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";

    public Creature(string name = "Unknown", int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature()
    {
    }

    public abstract string Greeting();

    public abstract int Power { get; }

    public void Upgrade()
    {
        if (Level < 10) Level++;
    }

    public string Go(Direction direction) => $"{direction.ToString().ToLower()}";

    public string[] Go(Direction[] directions)
    {
        return directions.Select(direction => Go(direction)).ToArray();
    }

    public string[] Go(string directions)
    {
        var parsedDirections = DirectionParser.Parse(directions);
        return Go(parsedDirections);
    }
}