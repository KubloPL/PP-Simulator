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
        set
        {
            if (_isNameSet) return;

            _name = value.Trim();
            if (_name.Length < 3)
                _name = _name.PadRight(3, '#');
            else if (_name.Length > 25)
                _name = _name.Substring(0, 25).TrimEnd().PadRight(3, '#');

            if (char.IsLower(_name[0]))
                _name = char.ToUpper(_name[0]) + _name.Substring(1);

            _isNameSet = true;
        }
    }

    public int Level
    {
        get => _level;
        set
        {
            if (_isLevelSet) return;

            _level = value < 1 ? 1 : (value > 10 ? 10 : value);
            _isLevelSet = true;
        }
    }

    public string Info => $"{Name} - [{Level}]";

    public Creature(string name = "Unknown", int level = 1)
    {
        Name = name;
        Level = level;
    }

    public Creature()
    {
    }

    public abstract void SayHi();

    public abstract int Power { get; }

    public void Upgrade()
    {
        if (Level < 10) Level++;
    }

    public void Go(Direction direction)
    {
        string directionText = direction.ToString().ToLower();
        Console.WriteLine($"{Name} goes {directionText}.");
    }

    public void Go(Direction[] directions)
    {
        foreach (var direction in directions)
        {
            Go(direction);
        }
    }

    public void Go(string directions)
    {
        var parsedDirections = DirectionParser.Parse(directions);
        Go(parsedDirections);
    }
}