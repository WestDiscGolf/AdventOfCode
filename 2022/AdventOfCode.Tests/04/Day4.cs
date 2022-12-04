using FluentAssertions;

namespace AdventOfCode.Tests._04;

public class Day4
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\04\\{filename}");

        var rows = raw.SplitOnNewLine();
        var pairs = rows.Select(x =>
        {
            var items = x.Split(',');
            return (items[0], items[1]);
        });

        var i = pairs.Select(x =>
        {
            var left = x.Item1.SplitOn('-', int.Parse);
            var right = x.Item2.SplitOn('-', int.Parse);
            
            if (left.x >= right.x
                && left.y <= right.y)
            {
                return true;
            }

            if (right.x >= left.x
                && right.y <= left.y)
            {
                return true;
            }

            return false;
        }).ToList();
        
        return i.Count(x => x);
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\04\\{filename}");
        
        return 0;
    }

    [Theory]
    [InlineData("Example.txt", 2)]
    [InlineData("Input01.txt", 556)]
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
