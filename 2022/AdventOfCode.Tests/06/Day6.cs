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
                if (!AnyDuplicates(q, 4))
                {
                    count = index;
                    break;
                }
            }
        }
        
        return count;
    }

    public bool AnyDuplicates(Queue<char> q, int count)
    {
        char[] copy = new char[count];
        q.CopyTo(copy, 0);
        var hash = new HashSet<char>(copy);
        return hash.Count < count;
    }

    public int Execute2(string input)
    {
        if (input.EndsWith("txt"))
        {
            input = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\06\\{input}");
        }

        int count = 0;

        Queue<char> q = new();

        foreach ((char c, int index) in input.Select((x, i) => (x, i + 1))) // work with 1 based!
        {
            if (index <= 14)
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
                if (!AnyDuplicates(q, 14))
                {
                    count = index;
                    break;
                }
            }
        }

        return count;
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
    [InlineData("mjqjpqmgbljsphdztnvjfqwrcgsmlb", 19)]
    [InlineData("bvwbjplbgvbhsrlpgdmjqwftvncz", 23)]
    [InlineData("nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg", 29)]
    [InlineData("zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw", 26)]
    [InlineData("Input01.txt", 2665)]
    public void Run2(string input, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(input);

        // Assert
        result.Should().Be(answer);
    }
}
