using System.Diagnostics;
using FluentAssertions;

namespace AdventOfCode.Tests._08;

public record Tree(int X, int Y, int Height);


public class Forest : List<Tree>
{
    public int? ColumnCount { get; set; }

    public int? RowCount { get; set; }

    public void Load(string[] rawLines)
    {
        // load into grid
        int rowNumber = 0;
        int column = 0;

        foreach (var rawLine in rawLines)
        {
            foreach (var height in rawLine.Select(x => int.Parse(x.ToString())))
            {
                Add(new Tree(column, rowNumber, height));
                column++;
            }

            if (ColumnCount is null)
            {
                ColumnCount = column;
            }

            column = 0;
            rowNumber++;
        }

        RowCount = rowNumber;
    }

    public int[] GetTreeHeights(Tree tree)
    {
        List<int> results = new();

        var p = tree with { X = tree.X - 1 };
        if (TryGetValue(p, out var left))
        {
            results.Add(left);
        }

        p = tree with { X = tree.X + 1 };
        if (TryGetValue(p, out var right))
        {
            results.Add(right);
        }

        p = tree with { Y = tree.Y - 1 };
        if (TryGetValue(p, out var up))
        {
            results.Add(up);
        }

        p = tree with { Y = tree.Y + 1 };
        if (TryGetValue(p, out var down))
        {
            results.Add(down);
        }

        return results.ToArray();
    }

    public bool TryGetValue(Tree tree, out int height)
    {
        height = default;
        var item = this.FirstOrDefault(gridPoint => gridPoint.X == tree.X && gridPoint.Y == tree.Y);
        if (item != null)
        {
            height = item.Height;
            return true;
        }

        return false;
    }
}

public class Day8
{
    public int Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\08\\{filename}");

        var forest = new Forest();
        forest.Load(raw.SplitOnNewLine());

        int count = 0;

        // now the grid is loaded, what do we need to do?
        foreach (var tree in forest)
        {
            bool added = false;

            var surroundingValues = forest.GetTreeHeights(tree);

            if (surroundingValues.Length < 4)
            {
                // if they don't have 4 surrounding values then it's on the edge
                count++;
            }
            else
            {
                // now we need to look at each of the internal trees
                // up
                int y = tree.Y;
                int altY = 1;

                while (y >= 0)
                {
                    if (forest.TryGetValue(tree with { Y = tree.Y - altY }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            y--;
                            altY++; // increase alt y so we move up the board
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        count++;
                        added = true;
                        break;
                    }
                }

                if (added)
                {
                    // move on to processing the next tree
                    continue;
                }

                // go down
                y = tree.Y;
                altY = 1;
                while (y <= forest.ColumnCount)
                {
                    if (forest.TryGetValue(tree with { Y = tree.Y + altY }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            y++;
                            altY++; // increase alt y so we move up the board
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        count++;
                        added = true;
                        break;
                    }
                }

                if (added)
                {
                    // move on to processing the next tree
                    continue;
                }

                // go left
                int x = tree.X;
                int altX = 1;

                while (x >= 0)
                {
                    if (forest.TryGetValue(tree with { X = tree.X - altX }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            x--;
                            altX++; // increase alt x so we move up the board
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        count++;
                        added = true;
                        break;
                    }
                }

                if (added)
                {
                    // move on to processing the next tree
                    continue;
                }

                // go right
                x = tree.X;
                altX = 1;
                while (x <= forest.RowCount)
                {
                    if (forest.TryGetValue(tree with { X = tree.X + altX }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            x++;
                            altX++; // increase alt y so we move up the board
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        count++;
                        added = true;
                        break;
                    }
                }
            }
        }

        return count;
    }

    public int Execute2(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\08\\{filename}");

        var forest = new Forest();
        forest.Load(raw.SplitOnNewLine());

        int maxScore = 0;

        // now the grid is loaded, what do we need to do?
        foreach (var tree in forest)
        {
            List<int> scenicScore = new();

            var surroundingValues = forest.GetTreeHeights(tree);

            if (surroundingValues.Length < 4)
            {
                // if they don't have 4 surrounding values then it's on the edge
            }
            else
            {
                // now we need to look at each of the internal trees
                // up
                int y = tree.Y;
                int altY = 1;

                int treeCount = 0;

                while (y >= 0)
                {
                    if (forest.TryGetValue(tree with { Y = tree.Y - altY }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            treeCount++;
                            y--;
                            altY++; // increase alt y so we move up the board
                        }
                        // if equal height it needs to stop

                        else if (height == tree.Height)
                        {
                            treeCount++;
                            //break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        break;
                    }
                }

                scenicScore.Add(treeCount);
                treeCount = 0;

                // go down
                y = tree.Y;
                altY = 1;
                while (y <= forest.ColumnCount)
                {
                    if (forest.TryGetValue(tree with { Y = tree.Y + altY }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            treeCount++;
                            y++;
                            altY++; // increase alt y so we move up the board
                        }

                        // if equal height it needs to stop

                        else if (height == tree.Height)
                        {
                            treeCount++;
                            //break;
                        }

                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        break;
                    }
                }

                scenicScore.Add(treeCount);
                treeCount = 0;

                // go left
                int x = tree.X;
                int altX = 1;

                while (x >= 0)
                {
                    if (forest.TryGetValue(tree with { X = tree.X - altX }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            treeCount++;
                            x--;
                            altX++; // increase alt x so we move up the board
                        }
                        // if equal height it needs to stop

                        else if (height == tree.Height)
                        {
                            treeCount++;
                            //break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        break;
                    }
                }

                scenicScore.Add(treeCount);
                treeCount = 0;

                // go right
                x = tree.X;
                altX = 1;
                while (x <= forest.RowCount)
                {
                    if (forest.TryGetValue(tree with { X = tree.X + altX }, out int height))
                    {
                        // if we're lower then move to the next tree
                        if (height < tree.Height)
                        {
                            // continue
                            treeCount++;
                            x++;
                            altX++; // increase alt y so we move up the board
                        }
                        // if equal height it needs to stop

                        else if (height == tree.Height)
                        {
                            treeCount++;
                            //break;
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        // we've reached the outside world and need to count the tree
                        break;
                    }
                }

                scenicScore.Add(treeCount);
            }

            // process checking scenic
            int i = 1;
            foreach (var i1 in scenicScore)
            {
                i *= i1;
            }

            if (i > maxScore)
            {
                maxScore = i;
            }
        }

        return maxScore;
    }

    [Theory]
    [InlineData("Example.txt", 21)]
    [InlineData("Input01.txt", 1684)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory(Skip = "Part 2 is WIP")]
    [InlineData("Example.txt", 8)]
    //[InlineData("Input01.txt", "")]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var result = Execute2(file);

        // Assert
        result.Should().Be(answer);
    }
}
