using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    /// <summary>
    /// Manages the simulation of creatures (or mappable entities) moving on a map.
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Simulation's map.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Entities (creatures or other mappable objects) moving on the map.
        /// </summary>
        public List<IMappable> Creatures { get; }

        /// <summary>
        /// Starting positions of the entities.
        /// </summary>
        public List<Point> Positions { get; }

        /// <summary>
        /// Sequence of moves for the entities.
        /// Moves are parsed to directions using the DirectionParser.
        /// Moves are assigned cyclically: the first move to the first creature, the second to the second, and so on.
        /// </summary>
        public string Moves { get; }

        /// <summary>
        /// Indicates whether all moves have been completed.
        /// </summary>
        public bool Finished { get; private set; } = false;

        /// <summary>
        /// The current entity that will perform the next move.
        /// </summary>
        public IMappable CurrentMappable
        {
            get
            {
                if (Finished || _currentMoveIndex >= _parsedMoves.Count)
                    throw new InvalidOperationException("All moves have been completed or the simulation is finished.");

                return Creatures[_currentMoveIndex % Creatures.Count];
            }
        }

        /// <summary>
        /// The name of the current move, in lowercase.
        /// </summary>
        public string CurrentMoveName
        {
            get
            {
                if (Finished || _currentMoveIndex >= _parsedMoves.Count)
                    throw new InvalidOperationException("All moves have been completed or the simulation is finished.");

                return _parsedMoves[_currentMoveIndex].ToString().ToLower();
            }
        }

        /// <summary>
        /// List of parsed directions from the moves string.
        /// </summary>
        private readonly List<Direction> _parsedMoves;

        /// <summary>
        /// Index of the current move in the parsed moves list.
        /// </summary>
        private int _currentMoveIndex = 0;

        /// <summary>
        /// Constructor for the simulation.
        /// Validates inputs and initializes the simulation state.
        /// Throws exceptions if:
        /// - The map is null,
        /// - The creatures list is null or empty,
        /// - The positions list is null or empty,
        /// - The number of creatures does not match the number of positions.
        /// </summary>
        public Simulation(Map map, List<IMappable> creatures, List<Point> positions, string moves)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map), "Map cannot be null.");

            if (creatures == null || creatures.Count == 0)
                throw new ArgumentException("Creatures list cannot be null or empty.", nameof(creatures));

            if (positions == null || positions.Count == 0)
                throw new ArgumentException("Positions list cannot be null or empty.", nameof(positions));

            if (creatures.Count != positions.Count)
                throw new ArgumentException("Number of creatures must match the number of starting positions.");
            
            Map = map;
            Creatures = creatures;
            Positions = positions;
            Moves = moves ?? string.Empty;

            // Initialize each creature's position on the map
            for (int i = 0; i < creatures.Count; i++)
            {
                IMappable creature = creatures[i];
                Point position = positions[i];

                if (!Map.Exist(position))
                    throw new ArgumentException($"Starting position {position} is out of map bounds for creature {creature.Name}.");

                creature.InitMapAndPosition(Map, position);
                
                Map.Add(creature, position);
            }

            // Parse the moves into a list of directions
            _parsedMoves = DirectionParser.Parse(moves);
        }

        /// <summary>
        /// Performs one turn in the simulation.
        /// The current creature executes its assigned move.
        /// Throws an error if:
        /// - The simulation is already finished,
        /// - There are no more moves to execute.
        /// </summary>
        public void Turn()
        {
            if (Finished)
                throw new InvalidOperationException("Cannot perform Turn. The simulation has already finished.");

            if (_currentMoveIndex >= _parsedMoves.Count)
            {
                Finished = true;
                throw new InvalidOperationException("No more moves to perform. The simulation has finished.");
            }

            // Determine which creature should move and in what direction
            IMappable creatureToMove = Creatures[_currentMoveIndex % Creatures.Count];
            Direction direction = _parsedMoves[_currentMoveIndex];

            // Execute the move
            creatureToMove.Go(direction);

            // Increment the move index
            _currentMoveIndex++;

            // Check if all moves are completed
            if (_currentMoveIndex >= _parsedMoves.Count)
                Finished = true;
        }
    }
} 