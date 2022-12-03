using FluentAssertions;

namespace AdventOfCode.Tests._03;

public class Day3
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\03\\{filename}");
        var rucksacks = raw.SplitOnNewLine();

        var next = rucksacks.Select(x => x.SplitInHalf());

        var commonChars = next.Select(x => x.left.Intersect(x.right).FirstOrDefault());

        var calculated = commonChars.Select(x => Convert(x));
        
        return calculated.Sum();
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\03\\{filename}");
        var rucksacks = raw.SplitOnNewLine();

        var grouped = rucksacks.Chunk(3);

        int result = 0;

        foreach (var group in grouped)
        {
            var i = group.Select(x => x).Cast<IEnumerable<char>>()
                .Aggregate((l1, l2) => l1.Intersect(l2)).FirstOrDefault();

            result += Convert(i);
        }

        return result;
    }

    [Theory]
    [InlineData("Example.txt", 157)]
    [InlineData("Input01.txt", 8185)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 70)]
    [InlineData("Input01.txt", 2817)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("vJrwpWtwJgWrhcsFMMfFFhFp", "vJrwpWtwJgWr", "hcsFMMfFFhFp")]
    public void SplitInHalfTests(string input, string expectedLeft, string expectedRight)
    {
        // Arrange

        // Act
        var (left, right) = input.SplitInHalf();

        // Assert
        left.Should().Be(expectedLeft);
        right.Should().Be(expectedRight);
    }

    [Theory]
    [InlineData('a', 1)]
    [InlineData('A', 27)]
    public void PriorityConverterTests(char c, int priority)
    {
        // Arrange

        // Act
        var result = Convert(c);

        // Assert
        result.Should().Be(priority);
    }

    public static int Convert(char c)
    {
        return c switch
        {
            'a' => 1,
            'b' => 2,
            'c' => 3,
            'd' => 4,
            'e' => 5,
            'f' => 6,
            'g' => 7,
            'h' => 8,
            'i' => 9,
            'j' => 10,
            'k' => 11,
            'l' => 12,
            'm' => 13,
            'n' => 14,
            'o' => 15,
            'p' => 16,
            'q' => 17,
            'r' => 18,
            's' => 19,
            't' => 20,
            'u' => 21,
            'v' => 22,
            'w' => 23,
            'x' => 24,
            'y' => 25,
            'z' => 26,
            'A' => 27,
            'B' => 28,
            'C' => 29,
            'D' => 30,
            'E' => 31,
            'F' => 32,
            'G' => 33,
            'H' => 34,
            'I' => 35,
            'J' => 36,
            'K' => 37,
            'L' => 38,
            'M' => 39,
            'N' => 40,
            'O' => 41,
            'P' => 42,
            'Q' => 43,
            'R' => 44,
            'S' => 45,
            'T' => 46,
            'U' => 47,
            'V' => 48,
            'W' => 49,
            'X' => 50,
            'Y' => 51,
            'Z' => 52,
            _ => 0
        };
    }
}
