using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AdventOfCode.Tests._06;

public class Day6
{
    public string Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{filename}");

        return "";
    }

    public string Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{filename}");

        return "";
    }

    [Theory]
    [InlineData("Example.txt", "")]
    [InlineData("Input01.txt", "")]
    public void Run(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", "")]
    [InlineData("Input01.txt", "")]
    public void Run2(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
