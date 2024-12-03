using System;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    public abstract class Creature : IMappable
    {
        private string _name = "Unknown";
        private int _level = 1;

        public string Name
        {
            get => _name;
            set => _name = Validator.Shortener(value, 3, 25, '#');
        }

        public int Level
        {
            get => _level;
            set => _level = Validator.Limiter(value, 1, 10);
        }

        public abstract string Info { get; }
        public override string ToString() => $"{GetType().Name.ToUpper()}: {Info}";

        public Creature(string name = "Unknown", int level = 1)
        {
            Name = name;
            Level = level;
        }

        public Creature()
        {
        }

        public abstract string Greeting();

        public abstract int Power { get; }

        public void Upgrade()
        {
            if (Level < 10) Level++;
        }

        private Map _map;
        private Point _position;

        public Map Map
        {
            get => _map;
            private set => _map = value;
        }

        public Point Position
        {
            get => _position;
            private set => _position = value;
        }

        public bool IsPlaced => _map != null;
        
        public void InitMapAndPosition(Map map, Point initialPosition)
        {
            if (map == null)
                throw new ArgumentNullException(nameof(map));
            if (!map.Exist(initialPosition))
                throw new ArgumentException("Initial position is out of map bounds", nameof(initialPosition));

            if (_map != null)
            {
                _map.Remove(this, _position);
            }

            _map = map;
            _position = initialPosition;
            _map.Add(this, _position);
        }
        
        public void Go(Direction direction)
        {
            if (!IsPlaced)
                return;

            Point newPosition = _map.Next(_position, direction);
            _map.Move(this, _position, newPosition);
            _position = newPosition;
        }
    }
}