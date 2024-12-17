// SimulationTurnLog.cs
using System.Collections.Generic;
using System.Drawing;

namespace Simulator
{
    /// <summary>
    /// State of map after single simulation turn.
    /// </summary>
    public class SimulationTurnLog
    {
        /// <summary>
        /// Text representation of the moving object in this turn.
        /// CurrentMappable.ToString()
        /// </summary>
        public required string Mappable { get; init; }

        /// <summary>
        /// Text representation of the move in this turn.
        /// CurrentMoveName.ToString();
        /// </summary>
        public required string Move { get; init; }

        /// <summary>
        /// Dictionary of IMappable.Symbol on the map in this turn.
        /// </summary>
        public required Dictionary<Point, char> Symbols { get; init; }
    }
}