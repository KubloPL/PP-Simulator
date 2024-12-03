using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Simulator;
using Simulator.Maps;
using SimConsole;

namespace SimConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            SmallSquareMap map = new SmallSquareMap(5);
            List<IMappable> creatures = new List<IMappable>
            {
                new Orc("Gorbag"),
                new Elf("Elandor")
            };
            List<Point> points = new List<Point>
            {
                new Point(2, 2),
                new Point(3, 1)
            };
            string moves = "dlrludl";

            Simulation simulation;
            try
            {
                simulation = new Simulation(map, creatures, points, moves);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error initializing simulation: {ex.Message}");
                return;
            }

            MapVisualizer mapVisualizer = new MapVisualizer(simulation.Map);

            Console.Clear(); 
            Console.WriteLine("Initial Map State:");
            mapVisualizer.Draw();
            Console.WriteLine();

            while (!simulation.Finished)
            {
                try
                {
                    IMappable current = simulation.CurrentMappable;
                    string moveName = simulation.CurrentMoveName;
                    Console.WriteLine($"Creature '{current.Name}' is moving {moveName}.");
                    simulation.Turn();

                    Console.WriteLine("Updated Map State:");
                    mapVisualizer.Draw();
                    Console.WriteLine();

                    Console.WriteLine("Press any key to proceed to the next turn...");
                    Console.ReadKey(true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during simulation: {ex.Message}");
                    break;
                }
            }

            Console.WriteLine("Simulation has finished.");
            Console.ReadKey();
        }
    }

    public class Orc : Creature, IMappable
    {
        public Orc(string name) : base(name) { }

        public override string Info => $"Name: {Name}, Level: {Level}";

        public override string Greeting() => "Grrrr!";

        public override int Power => Level * 5;

        public void Go(Direction direction) => base.Go(direction);

        public void InitMapAndPosition(Map map, Point point) => base.InitMapAndPosition(map, point);
    }

    public class Elf : Creature, IMappable
    {
        public Elf(string name) : base(name) { }

        public override string Info => $"Name: {Name}, Level: {Level}";

        public override string Greeting() => "Greetings!";

        public override int Power => Level * 7;

        public void Go(Direction direction) => base.Go(direction);

        public void InitMapAndPosition(Map map, Point point) => base.InitMapAndPosition(map, point);
    }
}