using System;
using System.Collections.Generic;
using System.Drawing;

namespace Simulator.Maps
{
    public abstract class Map
    {
        public int SizeX { get; }
        public int SizeY { get; }

        private readonly Rectangle bounds;
        private readonly Dictionary<Point, List<IMappable>> _mappables = new Dictionary<Point, List<IMappable>>();
        private readonly object _lock = new object();

        protected Map(int sizeX, int sizeY)
        {
            if (sizeX < 5)
                throw new ArgumentOutOfRangeException(nameof(sizeX), "Too narrow (X)");
            if (sizeY < 5)
                throw new ArgumentOutOfRangeException(nameof(sizeY), "Too short (Y)");

            SizeX = sizeX;
            SizeY = sizeY;
            bounds = new Rectangle(0, 0, sizeX - 1, sizeY - 1);
        }

        public virtual bool Exist(Point point) => bounds.Contains(point);

        public virtual void Add(IMappable mappable, Point point)
        {
            if (!Exist(point))
                throw new ArgumentException("Point is out of map bounds", nameof(point));

            lock (_lock)
            {
                if (!_mappables.ContainsKey(point))
                {
                    _mappables[point] = new List<IMappable>();
                }

                if (!_mappables[point].Contains(mappable))
                {
                    _mappables[point].Add(mappable);
                }
            }
        }

        public virtual void Remove(IMappable mappable, Point point)
        {
            lock (_lock)
            {
                if (_mappables.TryGetValue(point, out var list))
                {
                    list.Remove(mappable);
                    if (list.Count == 0)
                    {
                        _mappables.Remove(point);
                    }
                }
            }
        }

        public virtual void Move(IMappable mappable, Point from, Point to)
        {
            lock (_lock)
            {
                Remove(mappable, from);
                Add(mappable, to);
            }
        }

        public virtual IEnumerable<IMappable> At(Point point)
        {
            lock (_lock)
            {
                if (_mappables.TryGetValue(point, out var list))
                {
                    return new List<IMappable>(list);
                }
                return new List<IMappable>();
            }
        }

        public virtual IEnumerable<IMappable> At(int x, int y)
        {
            return At(new Point(x, y));
        }

        public virtual IEnumerable<IMappable> GetAllMappables()
        {
            lock (_lock)
            {
                foreach (var list in _mappables.Values)
                {
                    foreach (var mappable in list)
                    {
                        yield return mappable;
                    }
                }
            }
        }

        public abstract Point Next(Point start, Direction direction);
        public abstract Point NextDiagonal(Point start, Direction direction);
    }
}