using FluentAssertions;

namespace AdventOfCode.Tests._10;

public record Instruction(int Cycles, int Action);

public class Day10
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\10\\{filename}");
        var lines = raw.SplitOnNewLine();

        var instructions = lines.Select(x =>
        {
            return x.SplitBySpace()[0] switch
            {
                "addx" => new Instruction(2, int.Parse(x.SplitBySpace()[1])),
                _ => new Instruction(1, 0)
            };
        });

        // number of cycles
        var cycles = instructions.Select(x => x.Action).Sum();

        // starting value
        var result = 1;




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
