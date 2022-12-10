using FluentAssertions;

namespace AdventOfCode.Tests._10;

public record Instruction(Command Cmd, int Action);

public enum Command
{
    Noop,
    Addx
};

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
                "addx" => new Instruction(Command.Addx, int.Parse(x.SplitBySpace()[1])),
                _ => new Instruction(Command.Noop, 0)
            };
        });

        Dictionary<int, int> steps = new();
        int currentStep = 1;

        int registerX = 1;

        steps[currentStep] = registerX;

        foreach (var cmd in instructions)
        {
            switch (cmd.Cmd)
            {
                case Command.Noop:
                    steps[currentStep + 1] = registerX;
                    currentStep++;
                    break;

                case Command.Addx:

                    // keep the current register value
                    var temp = registerX;

                    // calculate the new value
                    registerX += cmd.Action;
                    
                    steps[currentStep + 1] = temp;
                    steps[currentStep + 2] = registerX;

                    currentStep += 2;
                    break;
            }
        }

        var total = GetValue(steps, 20)
                    + GetValue(steps, 60)
                    + GetValue(steps, 100)
                    + GetValue(steps, 140)
                    + GetValue(steps, 180)
                    + GetValue(steps, 220);
        
        return total;
    }

    public int GetValue(Dictionary<int, int> steps, int cycle) => steps[cycle] * cycle;

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\10\\{filename}");

       
        return 0;
    }

    [Theory]
    //[InlineData("Sample.txt", -1)]
    //[InlineData("Example.txt", 13140)]
    [InlineData("Input01.txt", 15680)]
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
