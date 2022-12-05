using System.Text.RegularExpressions;
using FluentAssertions;

namespace AdventOfCode.Tests._05;

public record Move(int Count, int From, int To);

public record Crate(string Value);

public class Ship : Dictionary<int, Stack<Crate>>
{
    public Ship(int stackCount)
    {
        for (var i = 1; i <= stackCount; i++)
        {
            Add(i, new Stack<Crate>());
        }
    }

    public void Push(int stack, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            this[stack].Push(new Crate(value));
        }
    }

    public bool Move(Move move)
    {
        for (int i = 0; i < move.Count; i++)
        {
            // pop
            var item = this[move.From].Pop();

            // push
            this[move.To].Push(item);
        }

        // just to use it in a select
        return true;
    }

    public string ReadTopCrates()
    {
        List<string> result = new();

        foreach (var key in Keys)
        {
            if (this[key].TryPeek(out var crate))
            {
                result.Add(crate.Value);
            }
        }

        return string.Join("", result);
    }
}

public class ShipTests
{
    [Fact]
    public void MoveOneFromTwoToOne()
    {
        // Arrange
        var sut = new Ship(3);
        sut.Push(1, "Z");
        sut.Push(1, "N");

        sut.Push(2, "M");
        sut.Push(2, "C");
        sut.Push(2, "D");

        sut.Push(3, "P");

        // Act
        sut.Move(new Move(1, 2, 1));

        // Assert
        sut[1].Count.Should().Be(3);
        sut[2].Count.Should().Be(2);
        sut[3].Count.Should().Be(1);

        sut[1].Select(x => x.Value).Should().BeEquivalentTo(new[] { "N", "Z", "D" });
        sut[2].Select(x => x.Value).Should().BeEquivalentTo(new[] { "M", "C" });
        sut[3].Select(x => x.Value).Should().BeEquivalentTo(new[] { "P" });
    }

    [Fact]
    public void ApplyMultipleMoves()
    {
        // Arrange
        var sut = new Ship(3);
        sut.Push(1, "Z");
        sut.Push(1, "N");

        sut.Push(2, "M");
        sut.Push(2, "C");
        sut.Push(2, "D");

        sut.Push(3, "P");

        // Act
        sut.Move(new Move(1, 2, 1));
        sut.Move(new Move(3, 1, 3));
        sut.Move(new Move(2, 2, 1));
        sut.Move(new Move(1, 1, 2));

        // Assert
        var result = sut.ReadTopCrates();
        result.Should().Be("CMZ");
    }
}

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
        _ = tuples.Select(x => ship.Move(x)).ToList();

        // read the top
        return ship.ReadTopCrates();
    }

    public string Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\05\\{filename}");

        return "";
    }

    [Theory]
    [InlineData("Example.txt", "CMZ")]
    [InlineData("Input01.txt", "CMZ")]
    public void Run(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory(Skip = "Yes")]
    [InlineData("Example.txt", "")]
    //[InlineData("Input01.txt", 876)]
    public void Run2(string file, string answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
