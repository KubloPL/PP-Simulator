using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;
using Simulator;

namespace SimConsole
{
    /// <summary>
    /// Responsible for visualizing the map and creatures on it.
    /// </summary>
    public class MapVisualizer
    {
        private readonly Map _map;

        /// <summary>
        /// Initializes a new instance of the <see cref="MapVisualizer"/> class.
        /// </summary>
        /// <param name="map">The map to visualize.</param>
        public MapVisualizer(Map map)
        {
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        /// <summary>
        /// Draws the current state of the map to the console.
        /// </summary>
        public void Draw()
        {
            // Create a 2D grid to represent the map
            char[,] grid = new char[_map.SizeY, _map.SizeX];

            // Initialize grid with empty spaces
            for (int y = 0; y < _map.SizeY; y++)
            {
                for (int x = 0; x < _map.SizeX; x++)
                {
                    grid[y, x] = ' ';
                }
            }

            // Populate grid with creatures
            foreach (var creature in _map.GetAllCreatures())
            {
                Point pos = creature.Position;
                if (_map.Exist(pos))
                {
                    // Determine the symbol based on creature type
                    char symbol = GetSymbol(creature);

                    // If multiple creatures are on the same position, mark as 'X'
                    if (grid[pos.Y, pos.X] == ' ' || grid[pos.Y, pos.X] == symbol)
                    {
                        grid[pos.Y, pos.X] = symbol;
                    }
                    else
                    {
                        grid[pos.Y, pos.X] = 'X';
                    }
                }
            }

            // Draw the top border
            Console.Write(Box.TopLeft);
            for (int x = 0; x < _map.SizeX; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < _map.SizeX - 1)
                    Console.Write(Box.TopMid);
            }
            Console.WriteLine(Box.TopRight);

            // Draw each row
            for (int y = 0; y < _map.SizeY; y++)
            {
                // Draw the vertical border
                Console.Write(Box.Vertical);

                // Draw the cells
                for (int x = 0; x < _map.SizeX; x++)
                {
                    Console.Write(grid[y, x]);
                    Console.Write(' ');
                }

                Console.WriteLine(Box.Vertical);
            }

            // Draw the bottom border
            Console.Write(Box.BottomLeft);
            for (int x = 0; x < _map.SizeX; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < _map.SizeX - 1)
                    Console.Write(Box.BottomMid);
            }
            Console.WriteLine(Box.BottomRight);
        }

        /// <summary>
        /// Determines the display symbol for a given creature.
        /// </summary>
        /// <param name="creature">The creature to get the symbol for.</param>
        /// <returns>A character representing the creature.</returns>
        private char GetSymbol(Creature creature)
        {
            return creature switch
            {
                Orc => 'O',
                Elf => 'E',
                _ => '?',
            };
        }
    }

    /// <summary>
    /// Extension methods for the Map class.
    /// </summary>
    public static class MapExtensions
    {
        /// <summary>
        /// Retrieves all creatures present on the map.
        /// </summary>
        /// <param name="map">The map to query.</param>
        /// <returns>A collection of all creatures on the map.</returns>
        public static IEnumerable<Creature> GetAllCreatures(this Map map)
        {
            var allCreatures = new List<Creature>();
            for (int y = 0; y < map.SizeY; y++)
            {
                for (int x = 0; x < map.SizeX; x++)
                {
                    foreach (var creature in map.At(x, y))
                    {
                        allCreatures.Add(creature);
                    }
                }
            }
            return allCreatures;
        }
    }
}