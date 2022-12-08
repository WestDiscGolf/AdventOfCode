using FluentAssertions;

namespace AdventOfCode.Tests._08;

public class Day8
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\08\\{filename}");

        // parse input into grid


        // recurse


        return 0;
    }

    [Theory]
    [InlineData("Example.txt", 21)]
    //[InlineData("Input01.txt", "")]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    public string Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\08\\{filename}");

        return "";
    }

    //[Theory]
    //[InlineData("Example.txt", "")]
    //[InlineData("Input01.txt", "")]
    //public void Run2(string file, string answer)
    //{
    //    // Arrange

    //    // Act
    //    var result = Execute2(file);

    //    // Assert
    //    result.Should().Be(answer);
    //}
}
