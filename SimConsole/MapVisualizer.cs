using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;
using Simulator;

namespace SimConsole
{
    /// <summary>
    /// Responsible for visualizing the map and mappables on it.
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

            // Populate grid with mappables
            foreach (var mappable in GetAllMappables())
            {
                Point pos = mappable.Position;
                if (_map.Exist(pos))
                {
                    char symbol = mappable.Symbol;

                    // If multiple mappables are on the same position, mark as 'X'
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

            // Draw each row with separators
            for (int y = 0; y < _map.SizeY; y++)
            {
                // Draw the vertical border and the cells
                Console.Write(Box.Vertical);
                for (int x = 0; x < _map.SizeX; x++)
                {
                    Console.Write(grid[y, x]);
                    if (x < _map.SizeX - 1)
                        Console.Write(Box.Vertical); // Vertical separator between cells
                }
                Console.WriteLine(Box.Vertical);

                // Draw the horizontal line between rows, except after the last row
                if (y < _map.SizeY - 1)
                {
                    Console.Write(Box.MidLeft);
                    for (int x = 0; x < _map.SizeX; x++)
                    {
                        Console.Write(Box.Horizontal);
                        if (x < _map.SizeX - 1)
                            Console.Write(Box.Cross);
                    }
                    Console.WriteLine(Box.MidRight);
                }
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
        /// Retrieves all mappables present on the map.
        /// </summary>
        /// <returns>A collection of all mappables on the map.</returns>
        private IEnumerable<IMappable> GetAllMappables()
        {
            var allMappables = new List<IMappable>();
            for (int y = 0; y < _map.SizeY; y++)
            {
                for (int x = 0; x < _map.SizeX; x++)
                {
                    foreach (var mappable in _map.At(x, y))
                    {
                        allMappables.Add(mappable);
                    }
                }
            }
            return allMappables;
        }
    }
}