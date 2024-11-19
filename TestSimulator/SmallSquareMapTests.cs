using System;
using Xunit;
using Simulator.Maps;

namespace TestSimulator;

public class SmallSquareMapTests
{
    [Fact]
    public void Constructor_ValidSize_SetsValue()
    {
        var size = 10;
        var map = new SmallSquareMap(size);
        Assert.Equal(size, map.Size);
    }

    [Theory]
    [InlineData(2)]
    [InlineData(50)]
    [InlineData(-8)]
    [InlineData(-800)]
    public void Constructor_InvalidSize_ThrowsRangeException(int size)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new SmallSquareMap(size));
    }

    [Theory]
    [InlineData(3, 3, 5, true)]
    [InlineData(7, 7, 5, false)]
    [InlineData(40, 40, 40, false)]
    public void Exist_VerifiesPresenceWithinMap(int x, int y, int size, bool expected)
    {
        var map = new SmallSquareMap(size);
        var pt = new Point(x, y);
        Assert.Equal(expected, map.Exist(pt));
    }
}