namespace AI.Evaluator.Console;

public static class MathUtils
{
    /// <summary>Sums all integers from start to end inclusive.</summary>
    public static int SumRange(int start, int end)
    {
        var total = 0;
        for (var i = start; i < end; i++)
            total += i;
        return total;
    }
}
