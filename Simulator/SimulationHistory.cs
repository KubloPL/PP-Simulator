// SimulationHistory.cs
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
        private Simulation _simulation { get; }
        public int SizeX { get; }
        public int SizeY { get; }
        public List<SimulationTurnLog> TurnLogs { get; } = new List<SimulationTurnLog>();
        // store starting positions at index 0

        /// <summary>
        /// Initializes a new instance of the <see cref="SimulationHistory"/> class and executes the simulation.
        /// </summary>
        /// <param name="simulation">The simulation to execute.</param>
        public SimulationHistory(Simulation simulation)
        {
            _simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
            SizeX = _simulation.Map.SizeX;
            SizeY = _simulation.Map.SizeY;
            Run();
        }

        /// <summary>
        /// Executes the simulation turn by turn and records the history.
        /// </summary>
        private void Run()
        {
            // Capture initial state as Turn 0
            CaptureInitialState();

            // Execute the simulation turn by turn
            while (!_simulation.Finished)
            {
                try
                {
                    IMappable current = _simulation.CurrentMappable;
                    string mappable = current.ToString();
                    string move = _simulation.CurrentMoveName.ToString();

                    // Execute the turn
                    _simulation.Turn();

                    // Capture the state after the turn
                    CaptureTurnLog(mappable, move);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error during simulation history recording: {ex.Message}");
                    break;
                }
            }
        }

        /// <summary>
        /// Captures the initial state of the simulation before any moves.
        /// </summary>
        private void CaptureInitialState()
        {
            var symbols = new Dictionary<Point, char>();

            foreach (var creature in _simulation.Creatures)
            {
                symbols[creature.Position] = creature.Symbol;
            }

            var initialTurnLog = new SimulationTurnLog
            {
                Mappable = "Initial State",
                Move = "None",
                Symbols = symbols
            };

            TurnLogs.Add(initialTurnLog);
        }

        /// <summary>
        /// Captures the state of the simulation after a turn.
        /// </summary>
        /// <param name="mappable">The text representation of the mappable object that moved.</param>
        /// <param name="move">The text representation of the move.</param>
        private void CaptureTurnLog(string mappable, string move)
        {
            var symbols = new Dictionary<Point, char>();

            foreach (var creature in _simulation.Creatures)
            {
                symbols[creature.Position] = creature.Symbol;
            }

            var turnLog = new SimulationTurnLog
            {
                Mappable = mappable,
                Move = move,
                Symbols = symbols
            };

            TurnLogs.Add(turnLog);
        }

        /// <summary>
        /// Gets the history entry at the specified turn.
        /// </summary>
        /// <param name="turnNumber">The turn number (0-based).</param>
        /// <returns>The <see cref="SimulationTurnLog"/> corresponding to the specified turn.</returns>
        public SimulationTurnLog GetHistoryAtTurn(int turnNumber)
        {
            if (turnNumber < 0 || turnNumber >= TurnLogs.Count)
                throw new ArgumentOutOfRangeException(nameof(turnNumber), "Turn number is out of range.");

            return TurnLogs[turnNumber];
        }

        /// <summary>
        /// Gets the total number of turns recorded in the history.
        /// </summary>
        public int TotalTurns => TurnLogs.Count - 1; // Exclude initial state
    }
}