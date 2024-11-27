using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    /// <summary>
    /// Manages the simulation of creatures moving on a map.
    /// </summary>
    public class Simulation
    {
        /// <summary>
        /// Simulation's map.
        /// </summary>
        public Map Map { get; }

        /// <summary>
        /// Creatures moving on the map.
        /// </summary>
        public List<Creature> Creatures { get; }

        /// <summary>
        /// Starting positions of creatures.
        /// </summary>
        public List<Point> Positions { get; }

        /// <summary>
        /// Cyclic list of creatures moves.
        /// Bad moves are ignored - use DirectionParser.
        /// First move is for first creature, second for second and so on.
        /// When all creatures make moves, 
        /// next move is again for first creature and so on.
        /// </summary>
        public string Moves { get; }

        /// <summary>
        /// Has all moves been done?
        /// </summary>
        public bool Finished { get; private set; } = false;

        /// <summary>
        /// Creature which will be moving current turn.
        /// </summary>
        public Creature CurrentCreature
        {
            get
            {
                if (Finished || _currentMoveIndex >= _parsedMoves.Count)
                    throw new InvalidOperationException("All moves have been completed or simulation is finished.");

                return Creatures[_currentMoveIndex % Creatures.Count];
            }
        }

        /// <summary>
        /// Lowercase name of direction which will be used in current turn.
        /// </summary>
        public string CurrentMoveName
        {
            get
            {
                if (Finished || _currentMoveIndex >= _parsedMoves.Count)
                    throw new InvalidOperationException("All moves have been completed or simulation is finished.");

                return _parsedMoves[_currentMoveIndex].ToString().ToLower();
            }
        }
        
        private readonly List<Direction> _parsedMoves;
        private int _currentMoveIndex = 0;

        /// <summary>
        /// Simulation constructor.
        /// Throws errors:
        /// - if creatures' list is empty,
        /// - if number of creatures differs from 
        ///   number of starting positions.
        /// </summary>
        public Simulation(Map map, List<Creature> creatures, 
                          List<Point> positions, string moves)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map), "Map cannot be null.");

            if (creatures == null || creatures.Count == 0)
                throw new ArgumentException("Creatures list cannot be null or empty.", nameof(creatures));

            if (positions == null || positions.Count == 0)
                throw new ArgumentException("Positions list cannot be null or empty.", nameof(positions));

            if (creatures.Count != positions.Count)
                throw new ArgumentException("Number of creatures must match number of starting positions.");

            Map = map;
            Creatures = creatures;
            Positions = positions;
            Moves = moves ?? string.Empty;
            
            for (int i = 0; i < creatures.Count; i++)
            {
                Creature creature = creatures[i];
                Point position = positions[i];

                if (!Map.Exist(position))
                    throw new ArgumentException($"Starting position {position} is out of map bounds for creature {creature.Name}.");

                creature.AssignMap(Map, position);
            }
            
            _parsedMoves = DirectionParser.Parse(moves);
        }

        /// <summary>
        /// Makes one move of current creature in current direction.
        /// Throws error if simulation is finished.
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
            
            Creature creatureToMove = Creatures[_currentMoveIndex % Creatures.Count];
            Direction direction = _parsedMoves[_currentMoveIndex];
            
            creatureToMove.Go(direction);
            
            _currentMoveIndex++;
            
            if (_currentMoveIndex >= _parsedMoves.Count)
                Finished = true;
        }
    }
}