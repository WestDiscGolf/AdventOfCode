using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace AdventOfCode.Tests._06;

public class Day6
{
    public int Execute(string input)
    {
        if (input.EndsWith("txt"))
        {
            input = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{input}");
        }

        int count = 0;

        Queue<char> q = new();

        foreach ((char c, int index) in input.Select((x, i) => (x, i + 1))) // work with 1 based!
        {
            if (index <= 4)
            {
                q.Enqueue(c);
            }
            else
            {
                // add the next one
                q.Enqueue(c);

                // get rid of the last one
                q.Dequeue();

                // any duplicates?
                if (!AnyDuplicates(q))
                {
                    count = index;
                    break;
                }
            }
        }
        
        return count;
    }

    public bool AnyDuplicates(Queue<char> q)
    {
        char[] copy = new char[4];
        q.CopyTo(copy, 0);
        var hash = new HashSet<char>(copy);
        return hash.Count < 4;
    }

    public string Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{filename}");

        return "";
    }

    [Theory]
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 7)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 5)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 10)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 11)]
    [InlineData("Input01.txt", 1655)]
    public void Run(string input, int answer)
    {
        // Arrange

        // Act
        var result = Execute(input);

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
