namespace Simulator.Maps
{
    public class BigMap : Map
    {
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