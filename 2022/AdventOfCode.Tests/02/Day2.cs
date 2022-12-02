using FluentAssertions;
using static AdventOfCode.Tests._02.Day2;

namespace AdventOfCode.Tests._02;

public class Day2
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\02\\{filename}");
        var games = raw.SplitOnNewLine();
        var groups = games.Select(x => x.SplitBySpace());
        var calculated = groups.Select(x => Calculate1(x[0], x[1]));
        return calculated.Sum();
    }

    public static int Calculate1(string opponent, string you)
    {
        var left = opponent.AsShape();
        var right = you.AsShape();

        int result = 0;

        if (left == right)
        {
            result = 3; // for a draw
        }
        else
        {
            switch (right)
            {
                case Shape.Rock:
                    result += left switch
                    {
                        Shape.Scissors => 6,
                        _ => 0
                    };
                    break;

                case Shape.Paper:
                    result += left switch
                    {
                        Shape.Rock => 6,
                        _ => 0
                    };
                    break;

                case Shape.Scissors:
                    result += left switch
                    {
                        Shape.Paper => 6,
                        _ => 0
                    };
                    break;
            }
        }

        // add the value selected
        result += (int)right;

        return result;
    }

    public static int Calculate2(string opponent, string requiredResult)
    {
        var left = opponent.AsShape();
        var right = requiredResult.AsResult();

        int result = 0;
        Shape? chosenShape = null;

        switch (right)
        {
            case RequiredResult.Win:
                chosenShape = left switch
                {
                    Shape.Paper => Shape.Scissors,
                    Shape.Rock => Shape.Paper,
                    Shape.Scissors => Shape.Rock,
                    _ => throw new NotSupportedException()
                };
                result += 6;
                break;
                
            case RequiredResult.Lose:
                chosenShape = left switch
                {
                    Shape.Paper => Shape.Rock,
                    Shape.Rock => Shape.Scissors,
                    Shape.Scissors => Shape.Paper,
                    _ => throw new NotSupportedException()
                };
                break;

            case RequiredResult.Draw:
                chosenShape = left;
                result += 3;
                break;
        }

        result += (int)chosenShape;

        return result;
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\02\\{filename}");
        var games = raw.SplitOnNewLine();
        var groups = games.Select(x => x.SplitBySpace());
        var calculated = groups.Select(x => Calculate2(x[0], x[1]));
        return calculated.Sum();
    }

    [Theory]
    [InlineData("A", "Y", 4)]
    [InlineData("B", "X", 1)]
    [InlineData("C", "Z", 7)]
    public void CalculateResult2(string opponent, string you, int expected)
    {
        // Arrange

        // Act
        var result = Calculate2(opponent, you);

        // Assert
        result.Should().Be(expected);
    }

    public enum Shape
    {
        Rock = 1,
        Paper = 2,
        Scissors = 3
    }

    public enum RequiredResult
    {
        Win,
        Lose,
        Draw
    }

    [Theory]
    [InlineData("Example.txt", 15)]
    [InlineData("Input01.txt", 9651)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 12)]
    [InlineData("Input01.txt", 10560)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("A", "Y", 8)]
    [InlineData("B", "X", 1)]
    [InlineData("C", "Z", 6)]
    public void CalculateResult(string opponent, string you, int expected)
    {
        // Arrange

        // Act
        var result = Calculate1(opponent, you);

        // Assert
        result.Should().Be(expected);
    }
}

public static class Day2Extensions
{
    public static Shape AsShape(this string value)
    {
        return value switch
        {
            "A" or "X" => Shape.Rock,
            "B" or "Y" => Shape.Paper,
            "C" or "Z" => Shape.Scissors,
            _ => throw new Exception("Not valid value for shape.")
        };
    }

    public static RequiredResult AsResult(this string value)
    {
        return value switch
        {
            "X" => RequiredResult.Lose,
            "Y" => RequiredResult.Draw,
            "Z" => RequiredResult.Win,
            _ => throw new Exception("Not valid value for shape.")
        };
    }
}
