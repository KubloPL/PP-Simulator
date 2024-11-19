using System;
using Xunit;
using Simulator;

namespace TestSimulator;

public class RectangleUnitTests
{
    [Theory]
    [InlineData(0, 0, 2, 2, 0, 0, 2, 2)]
    [InlineData(5, 1, 10, 8, 5, 1, 10, 8)]
    [InlineData(-2, -3, 3, 2, -2, -3, 3, 2)]
    [InlineData(10, 15, 5, 20, 5, 15, 10, 20)]
    public void Constructor_InitializesCorrectBounds(int x1, int y1, int x2, int y2, int ex1, int ey1, int ex2, int ey2)
    {
        var rect = new Rectangle(x1, y1, x2, y2);
        Assert.Equal(ex1, rect.X1);
        Assert.Equal(ey1, rect.Y1);
        Assert.Equal(ex2, rect.X2);
        Assert.Equal(ey2, rect.Y2);
    }

    [Theory]
    [InlineData(5, 5, 1, 1)]
    [InlineData(-3, -1, -3, -4)]
    [InlineData(7, 10, 5, 15)]
    public void Constructor_InvalidInput_ThrowsException(int x1, int y1, int x2, int y2)
    {
        Assert.Throws<ArgumentException>(() => new Rectangle(x1, y1, x2, y2));
    }

    [Theory]
    [InlineData(0, 0, 10, 10, 5, 5, true)]
    [InlineData(0, 0, 10, 10, 15, 15, false)]
    public void Contains_ChecksPointWithinBounds(int x1, int y1, int x2, int y2, int px, int py, bool expected)
    {
        var rect = new Rectangle(x1, y1, x2, y2);
        var pt = new Point(px, py);
        Assert.Equal(expected, rect.Contains(pt));
    }
}