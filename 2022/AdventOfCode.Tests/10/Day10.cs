using FluentAssertions;

namespace AdventOfCode.Tests._10;

public class Day10
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\10\\{filename}");

       

        return 0;
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\10\\{filename}");

       
        return 0;
    }

    [Theory]
    [InlineData("Example.txt", 21)]
    [InlineData("Input01.txt", 1684)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    //[Theory(Skip = "Part 2 is WIP")]
    //[InlineData("Example.txt", 8)]
    ////[InlineData("Input01.txt", "")]
    //public void Run2(string file, int answer)
    //{
    //    // Arrange

    //    // Act
    //    var result = Execute2(file);

    //    // Assert
    //    result.Should().Be(answer);
    //}
}
