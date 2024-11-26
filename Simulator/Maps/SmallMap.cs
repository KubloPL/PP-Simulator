using Simulator.Maps;
using Simulator;
using System.Collections.Generic;
using System.Drawing;

namespace Simulator.Maps
{
    public abstract class SmallMap : Map
    {
        private readonly Dictionary<Point, List<Creature>> _creatures = new Dictionary<Point, List<Creature>>();
        private readonly object _lock = new object();

        protected SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
            ValidateSize(sizeX, nameof(SizeX), "Too wide", 20);
            ValidateSize(sizeY, nameof(SizeY), "Too long", 20);
        }

        private static void ValidateSize(int value, string parameterName, string message, int maxAllowed)
        {
            if (value > maxAllowed)
                throw new ArgumentOutOfRangeException(parameterName, message);
        }

        public override void Add(Creature creature, Point point)
        {
            if (!Exist(point))
                throw new ArgumentException("Point is out of map bounds", nameof(point));

            lock (_lock)
            {
                if (!_creatures.ContainsKey(point))
                {
                    _creatures[point] = new List<Creature>();
                }
                _creatures[point].Add(creature);
            }
        }

        public override void Remove(Creature creature, Point point)
        {
            lock (_lock)
            {
                if (_creatures.TryGetValue(point, out var creatures))
                {
                    creatures.Remove(creature);
                    if (creatures.Count == 0)
                    {
                        _creatures.Remove(point);
                    }
                }
            }
        }

        public override void Move(Creature creature, Point from, Point to)
        {
            lock (_lock)
            {
                Remove(creature, from);
                Add(creature, to);
            }
        }

        public override IEnumerable<Creature> At(Point point)
        {
            lock (_lock)
            {
                if (_creatures.TryGetValue(point, out var creatures))
                {
                    return new List<Creature>(creatures);
                }
                return new List<Creature>();
            }
        }

        public override IEnumerable<Creature> At(int x, int y)
        {
            return At(new Point(x, y));
        }
    }
}