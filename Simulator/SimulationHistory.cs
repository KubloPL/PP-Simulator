using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    /// <summary>
    /// Represents the history of a simulation, allowing retrieval of the map state at any turn.
    /// </summary>
    public class SimulationHistory
    {
        /// <summary>
        /// Represents a single turn's data in the simulation.
        /// </summary>
        public class HistoryEntry
        {
            /// <summary>
            /// The turn number (starting from 1).
            /// </summary>
            public int TurnNumber { get; set; }

            /// <summary>
            /// The name of the creature that moved in this turn.
            /// </summary>
            public string MovedCreatureName { get; set; }

            /// <summary>
            /// The direction of the move.
            /// </summary>
            public Direction MoveDirection { get; set; }

            /// <summary>
            /// The positions of all creatures after this turn.
            /// </summary>
            public Dictionary<string, Point> Positions { get; set; }
        }

        /// <summary>
        /// List of history entries for each turn.
        /// </summary>
        private List<HistoryEntry> _history = new List<HistoryEntry>();

        /// <summary>
        /// Executes the simulation and records the history.
        /// </summary>
        /// <param name="simulation">The simulation to execute.</param>
        public SimulationHistory(Simulation simulation)
        {
            if (simulation == null)
                throw new ArgumentNullException(nameof(simulation));

            // Capture initial state as Turn 0 (no move)
            CaptureHistory(simulation, 0, null, Direction.Up); // Direction.Up is arbitrary here

            // Execute the simulation turn by turn
            while (!simulation.Finished)
            {
                try
                {
                    // Get current move info
                    IMappable current = simulation.CurrentMappable;
                    string movedCreatureName = current.Name;
                    string moveName = simulation.CurrentMoveName;

                    // Parse moveName back to Direction
                    Direction moveDirection = ParseDirection(moveName);

                    // Execute the turn
                    simulation.Turn();

                    // Capture the state after the turn
                    CaptureHistory(simulation, _history.Count, movedCreatureName, moveDirection);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during simulation history recording: {ex.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// Captures the state of the simulation at the given turn.
        /// </summary>
        /// <param name="simulation">The simulation instance.</param>
        /// <param name="turnNumber">The turn number.</param>
        /// <param name="movedCreatureName">The name of the creature that moved in this turn.</param>
        /// <param name="moveDirection">The direction of the move.</param>
        private void CaptureHistory(Simulation simulation, int turnNumber, string movedCreatureName, Direction moveDirection)
        {
            var entry = new HistoryEntry
            {
                TurnNumber = turnNumber,
                MovedCreatureName = movedCreatureName,
                MoveDirection = moveDirection,
                Positions = new Dictionary<string, Point>()
            };

            foreach (var creature in simulation.Creatures)
            {
                entry.Positions[creature.Name] = creature.Position;
            }

            _history.Add(entry);
        }

        /// <summary>
        /// Parses a move name string to a Direction enum.
        /// </summary>
        /// <param name="moveName">The move name as string (e.g., "up").</param>
        /// <returns>The corresponding Direction enum.</returns>
        private Direction ParseDirection(string moveName)
        {
            return moveName.ToLower() switch
            {
                "up" => Direction.Up,
                "down" => Direction.Down,
                "left" => Direction.Left,
                "right" => Direction.Right,
                _ => throw new ArgumentException($"Invalid move name: {moveName}"),
            };
        }

        /// <summary>
        /// Gets the history entry at the specified turn.
        /// </summary>
        /// <param name="turnNumber">The turn number (1-based).</param>
        /// <returns>The HistoryEntry corresponding to the specified turn.</returns>
        public HistoryEntry GetHistoryAtTurn(int turnNumber)
        {
            if (turnNumber < 0 || turnNumber >= _history.Count)
                throw new ArgumentOutOfRangeException(nameof(turnNumber), "Turn number is out of range.");

            return _history[turnNumber];
        }

        /// <summary>
        /// Gets the total number of turns recorded in the history.
        /// </summary>
        public int TotalTurns => _history.Count - 1; // Exclude initial state
    }
}