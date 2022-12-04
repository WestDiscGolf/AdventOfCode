using FluentAssertions;

namespace AdventOfCode.Tests;

public class RangeExtensionsTests
{
    [Theory]
    [InlineData(0)]
    [InlineData(8)]
    public void Contains(int input)
    {
        // Arrange
        var range = new Range(1, 7);

        // Act
        var result = range.Contains(input);

        // Assert
        result.Should().BeFalse();
    }

    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(5)]
    [InlineData(6)]
    [InlineData(7)]
    public void Contains2(int input)
    {
        // Arrange
        var range = new Range(1, 7);

        // Act
        var result = range.Contains(input);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ContainsRange()
    {
        // Arrange
        var range = new Range(1, 7);
        var value = new Range(1, 1);

        // Act
        var result = range.Contains(value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ContainsRange2()
    {
        // Arrange
        var range = new Range(1, 7);
        var value = new Range(7, 7);

        // Act
        var result = range.Contains(value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void ContainsRange3()
    {
        // Arrange
        var range = new Range(1, 7);
        var value = new Range(6, 10);

        // Act
        var result = range.Contains(value);

        // Assert
        result.Should().BeTrue();
    }

    [Fact]
    public void Overlaps()
    {
        // Arrange
        var range = new Range(1, 7);
        var value = new Range(6, 10);

        // Act
        var result = range.Overlaps(value);

        // Assert
        result.Should().BeTrue();
    }
}