using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Simulator;
using Simulator.Maps;

namespace SimConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            BigBounceMap map = new BigBounceMap(8, 6);

            List<IMappable> creatures = new List<IMappable>();
            List<Point> positions = new List<Point>();
            
            var orc = new Orc("Gorbag");
            creatures.Add(orc);
            positions.Add(new Point(1, 3));
            
            var elf = new Elf("Elandor");
            creatures.Add(elf);
            positions.Add(new Point(4, 4));
            
            for (int i = 0; i < 3; i++)
            {
                var rabbit = new Animals { Description = "Rabbit" };
                creatures.Add(rabbit);
                positions.Add(new Point(2 + i, 2));
            }
            
            for (int i = 0; i < 2; i++)
            {
                var eagle = new Birds { Description = "Eagle", CanFly = true };
                creatures.Add(eagle);
                positions.Add(new Point(5, 1 + i));
            }
            
            for (int i = 0; i < 2; i++)
            {
                var ostrich = new Birds { Description = "Ostrich", CanFly = false };
                creatures.Add(ostrich);
                positions.Add(new Point(6, 3 + i));
            }
            
            string moves = "uldruldruldruldruldrul";
            
            Simulation simulation;
            try
            {
                simulation = new Simulation(map, creatures, positions, moves);
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
                    Console.WriteLine($"'{current.Name}' is moving {moveName}.");
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
}