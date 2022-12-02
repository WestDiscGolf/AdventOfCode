using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AdventOfCode.Tests._02;

public class Day2
{
    public int Execute(string filename)
    {
        return 0;
    }

    public int Execute2(string filename) { return 1; }

    [Theory]
    [InlineData("Example.txt", 15)]
    //[InlineData("Input.txt", 68787)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 45000)]
    [InlineData("Input.txt", 198041)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
