namespace Simulator;

public class Orc : Creature
{
    private int rage = 1;
    private int huntCounter = 0;

    public int Rage
    {
        get => rage;
        init
        {
            rage = value < 1 ? 1 : (value > 10 ? 10 : value);
        }
    }

    public override int Power => 7 * Level + 3 * rage;

    public Orc() { }

    public Orc(string name, int level = 1, int rage = 1) : base(name, level)
    {
        Rage = rage;
    }

    public void Hunt()
    {
        huntCounter++;
        Console.WriteLine($"{Name} is hunting.");
        if (huntCounter % 3 == 0 && rage < 10)
        {
            rage++;
            Console.WriteLine($"Rage Up ({rage - 1} -> {rage})");
        }
    }

    public override void SayHi() => Console.WriteLine(
        $"Hi, I'm {Name}, my level is {Level}, my rage is {Rage}."
    );
}