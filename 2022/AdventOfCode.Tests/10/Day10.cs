using System.Text;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.Common.ExtensionFramework;

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

        #region CalculateSteps
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
        #endregion

        // now we've calculated the steps


        // calculate all the points

        // is the sprite visible when the cpu cycles.
        // if the pixel (or +-1) is on the cpu cycle it will be drawn

        List<string> crt = new();

        for (int cpuCycle = 1; cpuCycle < steps.Count; cpuCycle++)
        {
            // if register value is within +- 1 of current then '#'
            var registerValue = steps.TryGetValue(cpuCycle, out int v) ? v : 1;

            if (Math.Abs(registerValue - cpuCycle) < 2)
            {
                crt.Add("#");
            }
            else
            {
                crt.Add(".");
            }
        }

        var chunks = crt.Chunk(40);

        var sb = new StringBuilder();
        foreach (var chunk in chunks)
        {
            sb.AppendLine(string.Join("", chunk));
        }

        var s = sb.ToString();

        // 0 - 39 etc
        // chunk it up into groups of 40 .Chunk(40)

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

    //[Theory]
    //[InlineData("Example.txt", 8)]
    //[InlineData("Input01.txt", "")]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
