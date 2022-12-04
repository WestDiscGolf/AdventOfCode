namespace AdventOfCode.Tests;

/// <summary>
/// Inspired by https://www.codeproject.com/Articles/19028/Building-a-Generic-Range-class
/// </summary>
public static class RangeExtensions
{
    public static bool Contains(this Range range, int index) => range.Start.Value <= index && range.End.Value >= index;

    public static bool Contains(this Range range, Range value)
    {
        return range.Contains(range.Start.Value) || value.Contains(range.End.Value);
    }

    public static bool Overlaps(this Range range, Range value)
    {
        return range.Contains(value.Start.Value) || range.Contains(value.End.Value) || value.Contains(range.Start.Value) || value.Contains(range.End.Value);
    }
}