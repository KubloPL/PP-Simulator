using System;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    public class Animals : IMappable
    {
        private string _description = "Unknown";

        public string Description
        {
            get => _description;
            init => _description = Validator.Shortener(value, 3, 15, '#');
        }

        public uint Size { get; set; } = 3;

        public virtual string Info() => $"{Description} <{Size}>";

        public override string ToString() => $"{GetType().Name.ToUpper()}: {Info()}";
        
        public string Name => Description;

        protected Map map;
        protected Point position;

        public void InitMapAndPosition(Map map, Point point)
        {
            this.map = map;
            position = point;
        }

        public Point Position => position;

        public virtual void Go(Direction direction)
        {
            if (map == null)
                throw new Exception("Map not initialized.");

            position = map.Next(position, direction);
        }

        public virtual char Symbol => 'A'; // 'A' for animals
    }
}