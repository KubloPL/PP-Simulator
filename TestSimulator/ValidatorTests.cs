using System;
using Xunit;
using Simulator;

namespace TestSimulator;

public class InputValidatorTests
{
    [Theory]
    [InlineData(4, 1, 10, 4)]
    [InlineData(-5, 0, 5, 0)]
    [InlineData(12, 0, 10, 10)]
    public void Limiter_ClampsValueCorrectly(int value, int min, int max, int expected)
    {
        var result = Validator.Limiter(value, min, max);
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData("hello", 3, 7, '-', "Hello")]
    [InlineData("short text", 5, 8, '*', "Short te")]
    [InlineData("  trimmed  ", 6, 15, '_', "Trimmed__")]
    public void Shortener_AdjustsLengthCorrectly(string input, int min, int max, char filler, string expected)
    {
        var result = Validator.Shortener(input, min, max, filler);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void Shortener_EmptyString_ReturnsFillers()
    {
        var result = Validator.Shortener("", 4, 8, '_');
        Assert.Equal("____", result);
    }
}