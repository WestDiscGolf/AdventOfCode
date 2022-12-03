using System.Reflection.Metadata.Ecma335;

namespace AdventOfCode.Tests;

public static class Extensions
{
    public static string[] SplitOnNewLine(this string raw) => raw.Split(Environment.NewLine);

    public static string[] SplitOnDoubleNewLine(this string raw) => raw.Split(Environment.NewLine + Environment.NewLine);

    public static string[] SplitBySpace(this string raw) => raw.Split(' ', StringSplitOptions.RemoveEmptyEntries);

    public static (string left, string right) SplitInHalf(this string raw)
    {
        var middleIndex = raw.Length / 2;
        return (raw.Substring(0, middleIndex), raw.Substring(middleIndex));
    } 
}