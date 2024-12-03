namespace Simulator.Maps
{
    public interface IMappable
    {
        string Name { get; }
        Point Position { get; }
        void Go(Direction direction);
        void InitMapAndPosition(Map map, Point point);
    }
}