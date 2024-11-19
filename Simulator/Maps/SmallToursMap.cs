using System;
using Simulator.Maps;

public sealed class SmallTorusMap : Map
{
    private Rectangle _bounds;
    public int Size { get; }

    public SmallTorusMap(int size)
    {
        if (size < 5 || size > 20)
        {
            throw new ArgumentOutOfRangeException(nameof(size), "Rozmiar musi być z przedziału 5-20");
        }

        Size = size;
        _bounds = new Rectangle(0, 0, Size - 1, Size - 1);
    }

    public override bool Exist(Point point)
    {
        return _bounds.Contains(point);
    }

    public override Point Next(Point point, Direction direction)
    {
        return direction switch
        {
            Direction.Left when point.X == 0 => new Point(Size - 1, point.Y),
            Direction.Right when point.X == Size - 1 => new Point(0, point.Y),
            Direction.Down when point.Y == 0 => new Point(point.X, Size - 1),
            Direction.Up when point.Y == Size - 1 => new Point(point.X, 0),
            _ => Wrap(point.Next(direction)),
        };
    }

    public override Point NextDiagonal(Point point, Direction direction)
    {
        var nextDiagonal = point.NextDiagonal(direction);
        return Wrap(nextDiagonal);
    }

    private Point Wrap(Point point)
    {
        int wrappedX = (point.X + Size) % Size;
        int wrappedY = (point.Y + Size) % Size;
        return new Point(wrappedX, wrappedY);
    }
}