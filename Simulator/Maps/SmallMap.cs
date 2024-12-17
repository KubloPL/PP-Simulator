namespace Simulator.Maps
{
    public class SmallMap : Map
    {
        public SmallMap(int sizeX, int sizeY) : base(sizeX, sizeY)
        {
            ValidateSize(sizeX, nameof(SizeX), "Too wide", 20);
            ValidateSize(sizeY, nameof(SizeY), "Too long", 20);
        }

        private static void ValidateSize(int value, string parameterName, string message, int maxAllowed)
        {
            if (value > maxAllowed)
                throw new ArgumentOutOfRangeException(parameterName, message);
        }

        public override Point Next(Point start, Direction direction)
        {
            throw new NotImplementedException();
        }

        public override Point NextDiagonal(Point start, Direction direction)
        {
            throw new NotImplementedException();
        }
    }
}