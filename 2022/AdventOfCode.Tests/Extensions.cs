namespace AdventOfCode.Tests;

public static class Extensions
{
    public static string[] SplitOnNewLine(this string raw) => raw.Split(Environment.NewLine);

    public static string[] SplitOnDoubleNewLine(this string raw) => raw.Split(Environment.NewLine + Environment.NewLine);
}