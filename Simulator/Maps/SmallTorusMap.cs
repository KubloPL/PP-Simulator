using Simulator.Maps;

namespace Simulator.Maps;

public class SmallTorusMap : SmallMap
{
    public new int SizeX { get; }
    public new int SizeY { get; }

    public SmallTorusMap(int sizeX, int sizeY) : base(sizeX, sizeY)
    {
        SizeX = sizeX;
        SizeY = sizeY;
    }

    public override Point Next(Point p, Direction d)
    {
        Point moved = p.Next(d);
        int x = (moved.X < 0) ? SizeX - 1 : (moved.X >= SizeX ? 0 : moved.X);
        int y = (moved.Y < 0) ? SizeY - 1 : (moved.Y >= SizeY ? 0 : moved.Y);
        return new Point(x, y);
    }

    public override Point NextDiagonal(Point p, Direction d)
    {
        Point moved = p.NextDiagonal(d);
        int x = (moved.X < 0) ? SizeX - 1 : (moved.X >= SizeX ? 0 : moved.X);
        int y = (moved.Y < 0) ? SizeY - 1 : (moved.Y >= SizeY ? 0 : moved.Y);
        return new Point(x, y);
    }
}