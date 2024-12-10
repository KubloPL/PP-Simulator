using System;
using System.Drawing;

namespace Simulator.Maps
{
    public class BigBounceMap : BigMap
    {
        public BigBounceMap(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
        }

        public override Point Next(Point start, Direction direction)
        {
            Point moved = start.Next(direction);

            if (Exist(moved))
            {
                return moved;
            }
            else
            {
                Direction opposite = GetOppositeDirection(direction);
                Point bounced = start.Next(opposite);

                if (Exist(bounced))
                {
                    return bounced;
                }
                else
                {
                    return start;
                }
            }
        }

        public override Point NextDiagonal(Point start, Direction direction)
        {
            return start;
        }

        private Direction GetOppositeDirection(Direction direction)
        {
            return direction switch
            {
                Direction.Up => Direction.Down,
                Direction.Down => Direction.Up,
                Direction.Left => Direction.Right,
                Direction.Right => Direction.Left,
                _ => throw new ArgumentException("Unknown direction", nameof(direction)),
            };
        }
    }
}