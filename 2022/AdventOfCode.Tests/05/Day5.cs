using System.Text.RegularExpressions;
using FluentAssertions;

namespace AdventOfCode.Tests._05;

public class Day5
{
    public string Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\05\\{filename}");
        var rows = raw.SplitOnNewLine();

        // split starting position and moves
        var startingPosition = rows.TakeWhile(x => !string.IsNullOrEmpty(x));

        // flip
        startingPosition = startingPosition.Reverse().ToList();
        var count = startingPosition.First().SplitBySpace().Select(int.Parse).Max();
        Ship ship = new Ship(count);

        var index = new[] { 0, 1, 5, 9, 13, 17, 21, 25, 29, 33 };

/*
     1   2   3 
    [Z] [M] [P]
    [N] [C]    
        [D]    

 */


        foreach (var row in startingPosition.Skip(1)) // skip the line used above
        {
            for (int i = 1; i <= count; i++)
            {
                ship.Push(i, row[index[i]].ToString());
            }
        }

        // parse moves
        var moves = rows.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1); // skip the empty dividing line
        Regex regEx = new Regex(@"\d+");

        var tuples = moves.Select(move =>
        {
            var matches = regEx.Matches(move);
            var converted = matches.Select(x => int.Parse(x.Value)).ToArray();
            return new Move(converted[0], converted[1], converted[2]);
        });

        // Apply the moves
        _ = tuples.Select(x => ship.CrateMover9000(x)).ToList();

        // read the top
        return ship.ReadTopCrates();
    }

    public string Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\05\\{filename}");
        var rows = raw.SplitOnNewLine();

        // split starting position and moves
        var startingPosition = rows.TakeWhile(x => !string.IsNullOrEmpty(x));

        // flip
        startingPosition = startingPosition.Reverse().ToList();
        var count = startingPosition.First().SplitBySpace().Select(int.Parse).Max();
        Ship ship = new Ship(count);

        var index = new[] { 0, 1, 5, 9, 13, 17, 21, 25, 29, 33 };

        /*
             1   2   3 
            [Z] [M] [P]
            [N] [C]    
                [D]    

         */


        foreach (var row in startingPosition.Skip(1)) // skip the line used above
        {
            for (int i = 1; i <= count; i++)
            {
                ship.Push(i, row[index[i]].ToString());
            }
        }

        // parse moves
        var moves = rows.SkipWhile(x => !string.IsNullOrWhiteSpace(x)).Skip(1); // skip the empty dividing line
        Regex regEx = new Regex(@"\d+");

        var tuples = moves.Select(move =>
        {
            var matches = regEx.Matches(move);
            var converted = matches.Select(x => int.Parse(x.Value)).ToArray();
            return new Move(converted[0], converted[1], converted[2]);
        });

        // Apply the moves
        _ = tuples.Select(x => ship.CrateMover9001(x)).ToList();

        // read the top
        return ship.ReadTopCrates();
    }

    [Theory]
    [InlineData("Example.txt", "CMZ")]
    [InlineData("Input01.txt", "FJSRQCFTN")]
    public void Run(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", "MCD")]
    [InlineData("Input01.txt", "CJVLJQPHS")]
    public void Run2(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
