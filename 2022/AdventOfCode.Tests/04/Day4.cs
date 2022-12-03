using FluentAssertions;

namespace AdventOfCode.Tests._04;

public class Day4
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\04\\{filename}");
        
        return 0;
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\04\\{filename}");
        
        return 0;
    }

    [Theory]
    [InlineData("Example.txt", 157)]
    //[InlineData("Input01.txt", 8185)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 70)]
    //[InlineData("Input01.txt", 2817)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
