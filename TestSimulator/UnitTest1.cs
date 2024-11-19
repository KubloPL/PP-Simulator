using Simulator;

namespace TestSimulator;

public class DirectionParserTests
{
    [Fact]
    public void Parse_ShouldParseDirectionsCorrectly()
    {
        // Arrange
        string input = "URDL";
        // Act
        var result = DirectionParser.Parse(input);
        // Assert
        Assert.Equal(new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left }, result);
    }

    [Fact]
    public void Parse_ShouldHandleLowercaseLetters()
    {
        // Arrange
        string input = "urdl";
        // Act
        var result = DirectionParser.Parse(input);
        // Assert
        Assert.Equal(new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left }, result);
    }

    [Fact]
    public void Parse_ShouldReturnEmptyArrayForEmptyString()
    {
        // Arrange
        string input = "";
        // Act
        var result = DirectionParser.Parse(input);
        // Assert
        Assert.Empty(result);
    }

    [Theory]
    [InlineData("urdlx", new[] { Direction.Up, Direction.Right, Direction.Down, Direction.Left })]
    [InlineData("xxxdR lyyLTyu", new[] { Direction.Down, Direction.Right, Direction.Left, Direction.Left, Direction.Up })]
    public void Parse_ShouldIgnoreInvalidCharacters(string input, Direction[] expected)
    {
        // Arrange
        // Act
        var result = DirectionParser.Parse(input);
        // Assert
        Assert.Equal(expected, result);
    }
}