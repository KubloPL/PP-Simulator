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

            SmallTorusMap map = new SmallTorusMap(8, 6);
            
            List<IMappable> creatures = new List<IMappable>();
            
            var orc = new Orc("Gorbag");
            orc.InitMapAndPosition(map, new Point(1, 3));
            creatures.Add(orc);
            
            var elf = new Elf("Elandor");
            elf.InitMapAndPosition(map, new Point(4, 4));
            creatures.Add(elf);
            
            for (int i = 0; i < 3; i++)
            {
                var rabbit = new Animals { Description = "Rabbit" };
                rabbit.InitMapAndPosition(map, new Point(2 + i, 2));
                creatures.Add(rabbit);
            }
            
            for (int i = 0; i < 2; i++)
            {
                var eagle = new Birds { Description = "Eagle", CanFly = true };
                eagle.InitMapAndPosition(map, new Point(5, 1 + i));
                creatures.Add(eagle);
            }

            // Create and add flightless birds (ostriches)
            for (int i = 0; i < 2; i++)
            {
                var ostrich = new Birds { Description = "Ostrich", CanFly = false };
                ostrich.InitMapAndPosition(map, new Point(6, 3 + i));
                creatures.Add(ostrich);
            }
            
            List<Point> points = new List<Point>();
            Random rand = new Random();
            foreach (var _ in creatures)
            {
                points.Add(new Point(rand.Next(0, 8), rand.Next(0, 6)));
            }

            string moves = "dlrludludlrudlru"; 

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