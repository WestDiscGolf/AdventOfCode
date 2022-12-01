using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using FluentAssertions.Execution;

namespace AdventOfCode.Tests._01;

public class Day1
{
    /*
     *var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Input\\Day01-Part1.txt");
            var data = raw.Split(new[] {'\n'}, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).OrderBy(x => x).ToList();
     *
     *        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\Input\\{filename}");
        var rawLines = raw.Split(Environment.NewLine);
     */

    //public int Execute(string filename)
    //{
    //    var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\01\\{filename}");
    //    var items = raw.SplitOnNewLine();

    //    int calories = 0;
    //    List<int> elfs = new List<int>();
    //    foreach (var item in items)
    //    {
    //        if (item.Length > 0)
    //        {
    //            calories += int.Parse(item);
    //        }

    //        if (string.IsNullOrWhiteSpace(item))
    //        {
    //            // start a new one
    //            elfs.Add(calories);
    //            calories = 0;
    //        }
    //    }

    //    return elfs.Max();
    //}

    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\01\\{filename}");
        var elfsCalories = raw.SplitOnDoubleNewLine();
        var grouped = elfsCalories.Select(x => x.SplitOnNewLine());
        var totals = grouped.Select(x => x.Select(int.Parse));
        var totalled = totals.Select(x => x.Sum());
        return totalled.Max();
    }


    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\01\\{filename}");
        var items = raw.SplitOnNewLine();

        int calories = 0;
        List<int> elfs = new List<int>();

        foreach (var item in items)
        {
            if (item.Length > 0)
            {
                calories += int.Parse(item);
            }

            if (string.IsNullOrWhiteSpace(item))
            {
                // start a new one
                Add(elfs, calories);
                calories = 0;
            }
        }

        if (calories > 0)
        {
            Add(elfs, calories);
        }

        return elfs.Sum();
    }

    private void Add(List<int> items, int value)
    {
        if (items.Count < 3)
        {
            items.Add(value);
        }
        else
        {
            int minValue = items.Min();
            if (minValue <= value)
            {
                items.Remove(minValue);
                items.Add(value);
            }
        }
    }

    [Theory]
    [InlineData("Example.txt", 24000)]
    [InlineData("Input01.txt", 68787)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 45000)]
    [InlineData("Input01.txt", 198041)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}

public static class Extensions
{
    public static string[] SplitOnNewLine(this string raw) => raw.Split(Environment.NewLine);

    public static string[] SplitOnDoubleNewLine(this string raw) => raw.Split(Environment.NewLine + Environment.NewLine);
}