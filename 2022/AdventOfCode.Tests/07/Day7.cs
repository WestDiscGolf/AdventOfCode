using System.Collections.ObjectModel;
using FluentAssertions;

namespace AdventOfCode.Tests._07;

public record FileSystem
{
    public int Size { get; set; }
}

public record ElfFile(string Name) : FileSystem;

public record ElfDirectory(string Name) : FileSystem;

public record Command(string Cmd, string Args = "");

public class TreeNode<T>
{
    private readonly List<TreeNode<T>> _children = new();

    public TreeNode(T value) => Value = value;

    public TreeNode(T value, TreeNode<T> parent) => (Value, Parent) = (value, parent);

    public TreeNode<T> this[int i] => _children[i];

    public TreeNode<T>? Parent { get; }

    public T Value { get; }

    public ReadOnlyCollection<TreeNode<T>> Children => _children.AsReadOnly();

    public TreeNode<T> AddChild(T value)
    {
        var node = new TreeNode<T>(value, this);
        _children.Add(node);
        return node;
    }

    public TreeNode<T>[] AddChildren(params T[] values) => values.Select(AddChild).ToArray();

    public TreeNode<T> FindChild(Predicate<TreeNode<T>> predicate)
    {
        return _children.FirstOrDefault(child => predicate(child));
    }

    public bool RemoveChild(TreeNode<T> node) => _children.Remove(node);

    public void Traverse(Action<TreeNode<T>> action)
    {
        foreach (var child in _children)
        {
            child.Traverse(action);
        }
        action(this);
    }

    public TreeNode<T> InsertChild(TreeNode<T> parent, T value)
    {
        var node = new TreeNode<T>(value, parent); 
        parent._children.Add(node); 
        return node;
    }

    public IEnumerable<T> Flatten()
    {
        return new[] { Value }.Concat(_children.SelectMany(x => x.Flatten()));
    }
}

public class Day7
{
    public (int, int) Execute(string filename)
    {
        var raw = File.ReadAllText($"{Directory.GetCurrentDirectory()}\\07\\{filename}");

        var terminalOutput = raw.SplitOnNewLine();

        // setup the root
        var root = new TreeNode<FileSystem>(new ElfDirectory("/"));

        TreeNode<FileSystem> current = root;

        foreach (var terminalLine in terminalOutput.Skip(1)) // commands from after the setting to the root which is done above
        {
            switch (terminalLine)
            {
                case string s when s.StartsWith("$"):
                    var cmd = ProcessCmd(s);
                    if (cmd.Cmd == "cd")
                    {
                        switch (cmd.Args)
                        {
                            case "..":
                                current = current.Parent;
                                break;

                            default:
                                current = current.FindChild(x => x.Value is ElfDirectory dir && dir.Name.Equals(cmd.Args, StringComparison.OrdinalIgnoreCase));
                                break;
                        }
                    }
                    break;

                case string s when s.StartsWith("dir"):
                    var dir = CreateDirectory(s);
                    current.AddChild(dir);
                    break;

                default:
                    // make a file
                    var file = CreateFile(terminalLine);
                    current.AddChild(file);
                    break;
            }
        }

        root.Traverse(x =>
        {
            if (x.Parent is not null)
            {
                x.Parent.Value.Size += x.Value.Size;
            }
        });

        var dirsOver100000 = root.Flatten().Where(x => x is ElfDirectory dir && dir.Size <= 100_000).ToList();

        var hdd_size = 70_000_000;
        var rootsize = root.Value.Size;

        var unused = hdd_size - rootsize; // should be 21_618_835

        var required = 30_000_000 - unused;

        var itemsWhichAreBigger = root.Flatten().Where(x => x is ElfDirectory dir && dir.Size >= required).ToList();

        var min = itemsWhichAreBigger.Min(x => x.Size);

        return (dirsOver100000.Select(x => x.Size).Sum(), min);
    }

    private ElfFile CreateFile(string input) => new(input.SplitBySpace()[1]){ Size = int.Parse(input.SplitBySpace()[0])};

    private ElfDirectory CreateDirectory(string input) => new(input.SplitBySpace()[1]);

    private Command ProcessCmd(string input) =>
        input.SplitBySpace()[1] switch
        {
            "cd" => new Command("cd", input.SplitBySpace()[2]),
            "ls" => new Command("ls"),
            _ => throw new NotSupportedException("cmd input not supported")
        };

    [Theory]
    [InlineData("Example.txt", 95437)]
    [InlineData("Input01.txt", 1743217)]
    public void Run(string file, int answer)
    {
        // Arrange

        // Act
        var (result, _) = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    [Theory]
    [InlineData("Example.txt", 24933642)]
    [InlineData("Input01.txt", 0)]
    public void Run2(string file, int answer)
    {
        // Arrange

        // Act
        var (_, result) = Execute(file);

        // Assert
        result.Should().Be(answer);
    }

    public static List<object[]> DirTestData = new()
    {
        new object[] { "dir e", new ElfDirectory("e") },
        new object[] { "dir xyz", new ElfDirectory("xyz") },
    };

    [Theory]
    [MemberData(nameof(DirTestData))]
    public void TestParseDirectoryCreation(string input, ElfDirectory expected)
    {
        // Arrange

        // Act
        var directory = CreateDirectory(input);

        // Assert
        directory.Name.Should().Be(expected.Name);
    }

    public static List<object[]> FileTestData = new()
    {
        new object[] { "14848514 b.txt", new ElfFile("b.txt"){ Size = 14848514 } },
        new object[] { "8504156 c.dat", new ElfFile("c.dat"){ Size = 8504156 } },
        new object[] { "29116 f", new ElfFile("f"){ Size = 29116 } },
    };

    [Theory]
    [MemberData(nameof(FileTestData))]
    public void TestParseFileCreation(string input, ElfFile expected)
    {
        // Arrange

        // Act
        var file = CreateFile(input);

        // Assert
        file.Name.Should().Be(expected.Name);
        file.Size.Should().Be(expected.Size);
    }

    public static List<object[]> CmdTestData = new()
    {
        new object[] { "$ cd ..", new Command("cd", "..") },
        new object[] { "$ cd xyz", new Command("cd", "xyz") },
        new object[] { "$ ls", new Command("ls") },
    };

    [Theory]
    [MemberData(nameof(CmdTestData))]
    public void TestParseCommandCreation(string input, Command expected)
    {
        // Arrange

        // Act
        var cmd = ProcessCmd(input);

        // Assert
        cmd.Cmd.Should().Be(expected.Cmd);
        cmd.Args.Should().Be(expected.Args);
    }
}
