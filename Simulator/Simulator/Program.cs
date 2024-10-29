using Simulator;

class Program
{
    static void Main(string[] args)
    {
        Lab3a();
    }

    static void Lab3a()
    {
        Creature c = new() { Name = "   Shrek    ", Level = 20 };
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Creature("  ", -5);
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Creature("  donkey ") { Level = 7 };
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Creature("Puss in Boots – a clever and brave cat.");
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        c = new Creature("a                            troll name", 5);
        c.SayHi();
        c.Upgrade();
        Console.WriteLine(c.Info);

        var a = new Animals() { Description = "   Cats " };
        Console.WriteLine(a.Info);
        
        a = new Animals { Description = "Mice           are great", Size = 40 };
        Console.WriteLine(a.Info);
    }
}