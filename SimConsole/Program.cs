using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading;
using Simulator;
using Simulator.Maps;

namespace SimConsole
{
    class Program
    {
        // Toggle for display mode
        static bool useHistoryMode = false; // 'false' gives step-by-step mode
 
        
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;

            BigBounceMap map = new BigBounceMap(8, 6);

            List<IMappable> creatures = new List<IMappable>();
            List<Point> positions = new List<Point>();

            Dictionary<string, char> nameToSymbol = new Dictionary<string, char>();

            // Add Orc
            var orc = new Orc("Gorbag");
            creatures.Add(orc);
            positions.Add(new Point(1, 3));
            nameToSymbol[orc.Name] = orc.Symbol;

            // Add Elf
            var elf = new Elf("Elandor");
            creatures.Add(elf);
            positions.Add(new Point(4, 4));
            nameToSymbol[elf.Name] = elf.Symbol;

            // Add Animals with overlap check
            for (int i = 0; i < 3; i++)
            {
                var rabbit = new Animals { Description = $"Rabbit{i + 1}" };
                Point newPosition = new Point(2 + i, 2);

                // Check for overlap
                if (!positions.Contains(newPosition))
                    positions.Add(newPosition);
                else
                    positions.Add(new Point(3 + i, 3)); // Assign fallback position

                creatures.Add(rabbit);
                nameToSymbol[rabbit.Name] = rabbit.Symbol;
            }

            // Add Birds (Eagles and Ostriches)
            for (int i = 0; i < 2; i++)
            {
                var eagle = new Birds { Description = $"Eagle{i + 1}", CanFly = true };
                creatures.Add(eagle);
                positions.Add(new Point(5, 1 + i));
                nameToSymbol[eagle.Name] = eagle.Symbol;
            }

            for (int i = 0; i < 2; i++)
            {
                var ostrich = new Birds { Description = $"Ostrich{i + 1}", CanFly = false };
                creatures.Add(ostrich);
                positions.Add(new Point(6, 3 + i));
                nameToSymbol[ostrich.Name] = ostrich.Symbol;
            }

            // Ensure creatures and positions align
            if (creatures.Count != positions.Count)
                throw new Exception("Mismatch between creatures and positions count.");

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

            // Display simulation
            if (useHistoryMode)
            {
                SimulationHistory history = new SimulationHistory(simulation);

                List<int> turnsToDisplay = new List<int> { 5, 10, 15, 20 };

                foreach (int turn in turnsToDisplay)
                {
                    if (turn > history.TotalTurns)
                    {
                        Console.WriteLine($"Turn {turn} is beyond the total number of turns ({history.TotalTurns}).");
                        continue;
                    }

                    var entry = history.GetHistoryAtTurn(turn);

                    BigBounceMap tempMap = new BigBounceMap(map.SizeX, map.SizeY);

                    foreach (var kvp in entry.Positions)
                    {
                        string creatureName = kvp.Key;
                        Point pos = kvp.Value;

                        if (nameToSymbol.TryGetValue(creatureName, out char symbol))
                        {
                            IMappable tempMappable = new TempMappable(creatureName, symbol, pos);
                            tempMap.Add(tempMappable, pos);
                        }
                        else
                        {
                            Console.WriteLine($"Symbol for creature '{creatureName}' not found.");
                        }
                    }

                    MapVisualizer mapVisualizer = new MapVisualizer(tempMap);

                    Console.Clear();
                    Console.WriteLine($"Map State at Turn {turn}:");
                    Console.WriteLine($"'{entry.MovedCreatureName}' moved {entry.MoveDirection.ToString().ToLower()}.");
                    mapVisualizer.Draw();
                    Console.WriteLine();
                }

                Console.WriteLine("Simulation history displayed.");
            }
            else
            {
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

                        // Add delay for visualization
                        Thread.Sleep(500);

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
            }

            Console.ReadKey();
        }

        /// <summary>
        /// A temporary implementation of IMappable for visualization purposes.
        /// </summary>
        private class TempMappable : IMappable
        {
            public string Name { get; private set; }
            public Point Position { get; private set; }
            public char Symbol { get; private set; }

            public TempMappable(string name, char symbol, Point position)
            {
                Name = name;
                Symbol = symbol;
                Position = position;
            }

            public void Go(Direction direction)
            {
            }

            public void InitMapAndPosition(Map map, Point point)
            {
            }
        }
    }
}