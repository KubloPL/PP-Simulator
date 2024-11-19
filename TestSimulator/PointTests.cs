using Xunit;
namespace TestSimulator;
public class PointTests
{
    [Theory]
    [InlineData(1, 1, "(1, 1)")]
    [InlineData(15, 2, "(15, 2)")]
    [InlineData(37, 23, "(37, 23)")]
    [InlineData(-21, -98, "(-21, -98)")]
    public void ToString_ShouldReturnCorrectFormat(int x, int y, string expected)
    {
        var point = new Point(x, y);
        var result = point.ToString();
        Assert.Equal(expected, result);
    }
    [Theory]
    [InlineData(5, 5, Direction.Right, 6, 5)]
    [InlineData(5, 5, Direction.Left, 4, 5)]
    [InlineData(5, 5, Direction.Up, 5, 6)]
    [InlineData(5, 5, Direction.Down, 5, 4)]
    [InlineData(19, 19, Direction.Right, 20, 19)]
    [InlineData(0, 0, Direction.Left, -1, 0)]
    [InlineData(19, 19, Direction.Up, 19, 20)]
    [InlineData(0, 0, Direction.Down, 0, -1)]
    public void Next_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var point = new Point(x, y);
        var nextPoint = point.Next(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
    [Theory]
    [InlineData(5, 5, Direction.Right, 6, 4)]
    [InlineData(5, 5, Direction.Left, 4, 6)]
    [InlineData(5, 5, Direction.Up, 6, 6)]
    [InlineData(5, 5, Direction.Down, 4, 4)]
    [InlineData(19, 19, Direction.Right, 20, 18)]
    [InlineData(0, 0, Direction.Left, -1, 1)]
    [InlineData(19, 19, Direction.Up, 20, 20)]
    [InlineData(0, 0, Direction.Down, -1, -1)]
    public void NextDiagonal_ShouldReturnCorrectPoint(int x, int y, Direction direction, int expectedX, int expectedY)
    {
        var point = new Point(x, y);
        var nextPoint = point.NextDiagonal(direction);
        Assert.Equal(new Point(expectedX, expectedY), nextPoint);
    }
}