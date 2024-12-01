using FluentAssertions;
using FluentAssertions.Formatting;

namespace AdventOfCode.Tests;

public class UnitTest1
{
    public int Execute(string fileName)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Day01\\{fileName}");
        var lines = raw.SplitOnNewLine();
        var left = new List<int>();
        var right= new List<int>();

        foreach (var line in lines)
        {
            var (l, r) = line.SplitOn("   ", int.Parse);
            left.Add(l);
            right.Add(r);
        }

        var i = left.Order().Zip(right.Order());

        int total = 0;

        foreach (var (first, second) in i)
        {
            total += Math.Abs(first - second);
        }

        return total;
    }

    public int Execute2(string fileName)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Day01\\{fileName}");
        var lines = raw.SplitOnNewLine();

        var left = new List<int>();
        var right = new List<int>();

        foreach (var line in lines)
        {
            var (l, r) = line.SplitOn("   ", int.Parse);
            left.Add(l);
            right.Add(r);
        }

        int similarityScore = 0;

        foreach (var l in left)
        {
            var count = right.Count(x => x == l);
            similarityScore += (l * count);
        }

        return similarityScore;
    }

    [Theory]
    [InlineData("Example.txt", 11)]
    [InlineData("Input01.txt", 2970687)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 31)]
    [InlineData("Input01.txt", 198041)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("17113   23229", 17113, 23229)]
    [InlineData("3   4", 3,4)]
    [InlineData("4   3", 4,3)]
    public void Split(string input, int left, int right)
    {
        // Arrange
        
        // Act
        var (l, r) = input.SplitOn<int>("   ", int.Parse);

        // Assert
        l.Should().Be(left);
        r.Should().Be(right);
    }
}

public static class ProcessingExtensions
{
    public static string[] SplitOnNewLine(this string raw) => raw.Split(Environment.NewLine);

    public static (T x, T y) SplitOn<T>(this string raw, string separator, Func<string, T> convert) where T : struct
    {
        var index = raw.IndexOf(separator);
        return (convert(raw.Substring(0, index)), convert(raw.Substring(index + 1)));
    }
}
