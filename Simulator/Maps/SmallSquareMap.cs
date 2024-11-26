using Simulator.Maps;

namespace Simulator.Maps
{
    public class SmallSquareMap : SmallMap
    {
        public readonly int Size;

        public SmallSquareMap(int size) : base(size, size)
        {
            Size = size;
        }

        public override Point Next(Point p, Direction d)
        {
            var moved = p.Next(d);
            return Exist(moved) ? moved : p;
        }

        public override Point NextDiagonal(Point p, Direction d)
        {
            var moved = p.NextDiagonal(d);
            return Exist(moved) ? moved : p;
        }
        
        protected new bool Exist(Point point)
        {
            return point.X >= 0 && point.X < Size && point.Y >= 0 && point.Y < Size;
        }
    }
}