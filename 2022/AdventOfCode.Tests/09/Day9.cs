using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventOfCode.Tests._05;

namespace AdventOfCode.Tests._09;

public static class Extensions
{
    public static Instruction ParseInstruction(this string instruction)
    {
        var parts = instruction.SplitBySpace();
        var direction = parts[0] switch
        {
            "R" => Direction.Right,
            "D" => Direction.Down,
            "L" => Direction.Left,
            "U" => Direction.Up,
            _ => throw new NotSupportedException("trying to parse an invalid direction!")
        };
        return new(direction, int.Parse(parts[1]));
    }
}

public enum Direction
{
    Right,
    Down,
    Left,
    Up,
    None
}

public record Instruction(Direction Direction, int Moves);

public record Coordinates(int X, int Y);

public class Day9
{
    private Coordinates H;
    private (Coordinates, Direction) HLastSighting;
    private Coordinates T;
    private (Coordinates, int)[,] grid;

    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\09\\{filename}");
        var lines = raw.SplitOnNewLine();
        var instructions = lines.Select(x => x.ParseInstruction());

        // assumption is x by x grid starting bottom left as per the description.
        
        // am I near it?
        // am i near it if I go into any of the 8 places around me?

        // make some aritary size
        grid = new (Coordinates, int)[10000, 10000];

        // X => left/right
        // Y => up/down

        H = new(5000, 5000); // bottom left
        T = new(5000, 5000); // bottom left
        Seen(T);
        
        HLastSighting = new(new(5000,5000), Direction.None); // bottom left

        foreach (var instruction in instructions)
        {
            for (int i = 0; i < instruction.Moves; i++)
            {
                MoveH(instruction.Direction);
                MoveF(instruction.Direction);
            }
        }

        int count = 0;

        for (var i = 0; i < 5; i++)
        {
            for (var j = 0; j < 5; j++)
            {
                if (grid[i, j].Item2 > 0)
                {
                    count++;
                }
            }
        }

        return count;
    }

    private void MoveH(Direction instructionDirection)
    {
        // set last sighting before moving
        HLastSighting = (H, instructionDirection);

        // move the Head
        H = Move(H, instructionDirection);
    }
    
    private void MoveF(Direction instructionDirection)
    {
        // u/d/l/r are we still in contact?
        // if yes, all done
        if ((Math.Abs(H.X - T.X) <= 1) && (Math.Abs(H.Y - T.Y) <= 1))
        {
            return;
        }
        else
        {
            // if no, check last sighting
            var (coordinates, direction) = HLastSighting;

            T = coordinates;
        }

        Seen(T);
    }

    private void Seen(Coordinates coordinates)
    {
        grid[coordinates.X, coordinates.Y].Item2++;
    }

    public Coordinates Move(Coordinates coordinates, Direction direction)
    {
        switch (direction)
        {
            case Direction.Right:
                int r = coordinates.X + 1;
                return coordinates with { X = r };

            case Direction.Left:
                int l = coordinates.X - 1;
                return coordinates with { X = l };

            case Direction.Up:
                int u = coordinates.Y + 1;
                return coordinates with { Y = u };

            case Direction.Down:
                int d = coordinates.Y - 1;
                return coordinates with { Y = d };
        }

        throw new NotSupportedException("Trying to move in a direction which is not supported!");
    }

    [Theory]
    //[InlineData("Example.txt", 13)]
    [InlineData("Input01.txt", -1)]
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
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{filename}");

        return "";
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
