using Simulator.Maps;
using Simulator;
using System.Collections.Generic;
using System.Drawing;

namespace Simulator.Maps
{
    public abstract class SmallMap : Map
    {
        private readonly Dictionary<Point, List<IMappable>> _mappables = new Dictionary<Point, List<IMappable>>();
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

        public override void Add(IMappable mappable, Point point)
        {
            if (!Exist(point))
                throw new ArgumentException("Point is out of map bounds", nameof(point));

            lock (_lock)
            {
                if (!_mappables.ContainsKey(point))
                {
                    _mappables[point] = new List<IMappable>();
                }
                _mappables[point].Add(mappable);
            }
        }

        public override void Remove(IMappable mappable, Point point)
        {
            lock (_lock)
            {
                if (_mappables.TryGetValue(point, out var mappables))
                {
                    mappables.Remove(mappable);
                    if (mappables.Count == 0)
                    {
                        _mappables.Remove(point);
                    }
                }
            }
        }

        public override void Move(IMappable mappable, Point from, Point to)
        {
            lock (_lock)
            {
                Remove(mappable, from);
                Add(mappable, to);
            }
        }

        public override IEnumerable<IMappable> At(Point point)
        {
            lock (_lock)
            {
                if (_mappables.TryGetValue(point, out var mappables))
                {
                    return new List<IMappable>(mappables);
                }
                return new List<IMappable>();
            }
        }

        public override IEnumerable<IMappable> At(int x, int y)
        {
            return At(new Point(x, y));
        }
    }
}