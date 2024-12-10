using System;
using System.Collections.Generic;
using System.Drawing;
using Simulator.Maps;

namespace Simulator.Maps
{
    public class BigMap : Map
    {
        protected readonly Dictionary<Point, List<IMappable>> Mappables = new Dictionary<Point, List<IMappable>>();
        private readonly object _lock = new object();

        public BigMap(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
            ValidateSize(sizeX, nameof(sizeX), "Too wide", 1000);
            ValidateSize(sizeY, nameof(sizeY), "Too long", 1000);
        }

        private static void ValidateSize(int value, string parameterName, string message, int maxAllowed)
        {
            if (value < 5)
                throw new ArgumentOutOfRangeException(parameterName, "Size must be at least 5.");
            if (value > maxAllowed)
                throw new ArgumentOutOfRangeException(parameterName, message);
        }

        public override void Add(IMappable mappable, Point point)
        {
            if (!Exist(point))
                throw new ArgumentException("Point is out of map bounds", nameof(point));

            lock (_lock)
            {
                if (!Mappables.ContainsKey(point))
                {
                    Mappables[point] = new List<IMappable>();
                }
                Mappables[point].Add(mappable);
            }
        }

        public override void Remove(IMappable mappable, Point point)
        {
            lock (_lock)
            {
                if (Mappables.TryGetValue(point, out var list))
                {
                    list.Remove(mappable);
                    if (list.Count == 0)
                    {
                        Mappables.Remove(point);
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
                if (Mappables.TryGetValue(point, out var list))
                {
                    return new List<IMappable>(list);
                }
                return new List<IMappable>();
            }
        }

        public override IEnumerable<IMappable> At(int x, int y)
        {
            return At(new Point(x, y));
        }

        public override IEnumerable<IMappable> GetAllMappables()
        {
            lock (_lock)
            {
                foreach (var list in Mappables.Values)
                {
                    foreach (var mappable in list)
                    {
                        yield return mappable;
                    }
                }
            }
        }

        public override Point Next(Point start, Direction direction)
        {
            throw new NotImplementedException("Method Next should be implemented in a derived class.");
        }

        public override Point NextDiagonal(Point start, Direction direction)
        {
            throw new NotImplementedException("Method NextDiagonal Should be implemented in a derived class.");
        }
    }
}