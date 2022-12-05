namespace AdventOfCode.Tests._05;

public class Ship : Dictionary<int, Stack<Crate>>
{
    public Ship(int stackCount)
    {
        for (var i = 1; i <= stackCount; i++)
        {
            Add(i, new Stack<Crate>());
        }
    }

    public void Push(int stack, string value)
    {
        if (!string.IsNullOrWhiteSpace(value))
        {
            this[stack].Push(new Crate(value));
        }
    } 

    public bool CrateMover9000(Move move)
    {
        for (int i = 0; i < move.Count; i++)
        {
            // pop
            var item = this[move.From].Pop();

            // push
            this[move.To].Push(item);
        }

        // just to use it in a select
        return true;
    }

    public bool CrateMover9001(Move move)
    {
        Stack<Crate> inner = new();

        for (int i = 0; i < move.Count; i++)
        {
            // pop
            var item = this[move.From].Pop();

            inner.Push(item);
        }

        for (int i = 0; i < move.Count; i++)
        {
            this[move.To].Push(inner.Pop());
        }

        // just to use it in a select
        return true;
    }

    public string ReadTopCrates()
    {
        List<string> result = new();

        foreach (var key in Keys)
        {
            if (this[key].TryPeek(out var crate))
            {
                result.Add(crate.Value);
            }
        }

        return string.Join("", result);
    }
}