using FluentAssertions;

namespace AdventOfCode.Tests._05;

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
        sut.CrateMover9000(new Move(1, 2, 1));

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
        sut.CrateMover9000(new Move(1, 2, 1));
        sut.CrateMover9000(new Move(3, 1, 3));
        sut.CrateMover9000(new Move(2, 2, 1));
        sut.CrateMover9000(new Move(1, 1, 2));

        // Assert
        var result = sut.ReadTopCrates();
        result.Should().Be("CMZ");
    }

    [Fact]
    public void MoveOneFromTwoToOneCrateMover9001()
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
        sut.CrateMover9001(new Move(1, 2, 1));

        // Assert
        sut[1].Count.Should().Be(3);
        sut[2].Count.Should().Be(2);
        sut[3].Count.Should().Be(1);

        sut[1].Select(x => x.Value).Should().BeEquivalentTo(new[] { "N", "Z", "D" });
        sut[2].Select(x => x.Value).Should().BeEquivalentTo(new[] { "M", "C" });
        sut[3].Select(x => x.Value).Should().BeEquivalentTo(new[] { "P" });
    }

    [Fact]
    public void ApplyMultipleMovesCrateMover9001()
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
        sut.CrateMover9001(new Move(1, 2, 1));
        sut.CrateMover9001(new Move(3, 1, 3));
        sut.CrateMover9001(new Move(2, 2, 1));
        sut.CrateMover9001(new Move(1, 1, 2));

        // Assert
        var result = sut.ReadTopCrates();
        result.Should().Be("MCD");
    }
}