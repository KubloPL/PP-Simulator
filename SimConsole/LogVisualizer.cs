// LogVisualizer.cs
using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;
using Simulator;

namespace SimConsole
{
    /// <summary>
    /// Responsible for visualizing the simulation history.
    /// </summary>
    internal class LogVisualizer
    {
        private readonly SimulationHistory _log;
        private readonly int _sizeX;
        private readonly int _sizeY;

        /// <summary>
        /// Initializes a new instance of the <see cref="LogVisualizer"/> class.
        /// </summary>
        /// <param name="log">The simulation history to visualize.</param>
        public LogVisualizer(SimulationHistory log)
        {
            _log = log ?? throw new ArgumentNullException(nameof(log));
            _sizeX = _log.SizeX;
            _sizeY = _log.SizeY;
        }

        /// <summary>
        /// Draws the state of the map at the specified turn index.
        /// </summary>
        /// <param name="turnIndex">The index of the turn to visualize.</param>
        public void Draw(int turnIndex)
        {
            // Retrieve the turn log
            SimulationTurnLog turnLog;
            try
            {
                turnLog = _log.GetHistoryAtTurn(turnIndex);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"Turn index {turnIndex} is out of range.");
                return;
            }

            // Create a 2D grid to represent the map
            char[,] grid = new char[_sizeY, _sizeX];

            // Initialize grid with empty spaces
            for (int y = 0; y < _sizeY; y++)
            {
                for (int x = 0; x < _sizeX; x++)
                {
                    grid[y, x] = ' ';
                }
            }

            // Populate grid with symbols from the turn log
            foreach (var kvp in turnLog.Symbols)
            {
                Point pos = kvp.Key;
                char symbol = kvp.Value;

                if (IsValidPosition(pos))
                {
                    if (grid[pos.Y, pos.X] == ' ')
                        grid[pos.Y, pos.X] = symbol;
                    else
                        grid[pos.Y, pos.X] = 'X'; // Overlap indicator
                }
                else
                {
                    Console.WriteLine($"Warning: Symbol '{symbol}' at invalid position {pos}.");
                }
            }

            // Draw the top border
            Console.Write(Box.TopLeft);
            for (int x = 0; x < _sizeX; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < _sizeX - 1)
                    Console.Write(Box.TopMid);
            }
            Console.WriteLine(Box.TopRight);

            // Draw each row with separators
            for (int y = 0; y < _sizeY; y++)
            {
                // Draw the vertical border and the cells
                Console.Write(Box.Vertical);
                for (int x = 0; x < _sizeX; x++)
                {
                    Console.Write(grid[y, x]);
                    if (x < _sizeX - 1)
                        Console.Write(Box.Vertical); // Vertical separator between cells
                }
                Console.WriteLine(Box.Vertical);

                // Draw the horizontal line between rows, except after the last row
                if (y < _sizeY - 1)
                {
                    Console.Write(Box.MidLeft);
                    for (int x = 0; x < _sizeX; x++)
                    {
                        Console.Write(Box.Horizontal);
                        if (x < _sizeX - 1)
                            Console.Write(Box.Cross);
                    }
                    Console.WriteLine(Box.MidRight);
                }
            }

            // Draw the bottom border
            Console.Write(Box.BottomLeft);
            for (int x = 0; x < _sizeX; x++)
            {
                Console.Write(Box.Horizontal);
                if (x < _sizeX - 1)
                    Console.Write(Box.BottomMid);
            }
            Console.WriteLine(Box.BottomRight);

            // Display move information
            if (turnLog.Move != "None")
            {
                Console.WriteLine($"'{turnLog.Mappable}' moved {turnLog.Move.ToLower()}.");
            }
            else
            {
                Console.WriteLine("Initial Map State (no moves).");
            }
        }

        /// <summary>
        /// Checks if the given position is within the bounds of the map.
        /// </summary>
        /// <param name="pos">The position to check.</param>
        /// <returns>True if the position is valid; otherwise, false.</returns>
        private bool IsValidPosition(Point pos)
        {
            return pos.X >= 0 && pos.X < _sizeX && pos.Y >= 0 && pos.Y < _sizeY;
        }
    }
}