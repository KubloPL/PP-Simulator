using System;
using System.Drawing;
using Simulator.Maps;

namespace Simulator
{
    public class Birds : Animals
    {
        private readonly bool _canFly = true;

        public bool CanFly
        {
            get => _canFly;
            init => _canFly = value;
        }

        public override string Info()
        {
            string flyAbility = CanFly ? "fly+" : "fly-";
            return $"{Description} ({flyAbility}) <{Size}>";
        }

        public override void Go(Direction direction)
        {
            if (map == null)
                throw new Exception("Map not initialized.");

            if (CanFly)
            {
                position = map.Next(position, direction);
                position = map.Next(position, direction);
            }
            else
            {
                position = map.NextDiagonal(position, direction);
            }
        }

        public override char Symbol => CanFly ? 'B' : 'b'; // 'B' for flying birds, 'b' for non-flying birds
    }
}