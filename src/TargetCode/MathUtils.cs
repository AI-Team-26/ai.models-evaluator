using System.Text;

namespace TargetCode;

public static class MathUtils
{
    /// <summary>Sums all integers from start to end inclusive.</summary>
    public static int SumRange(int start, int end)
    {
        var total = 0;
        for (var i = start; i <= end; i++)
            total += i;
        return total;
    }

    /// <summary>Splits a CSV string into individual fields, respecting quoted values.</summary>
    public static string[] SplitCsv(string input)
    {
        if (string.IsNullOrEmpty(input))
            return Array.Empty<string>();

        var fields = new List<string>();
        var current = new StringBuilder();
        bool inQuotes = false;

        foreach (char c in input)
        {
            if (c == '\"')
            {
                if (inQuotes)
                {
                    // Exiting quotes - add the quote to current field
                    current.Append(c);
                }
                else
                {
                    // Entering quotes - add the quote to current field
                    current.Append(c);
                }
                inQuotes = !inQuotes;
            }
            else if (c == ',' && !inQuotes)
            {
                fields.Add(current.ToString().Trim());
                current.Clear();
            }
            else
            {
                current.Append(c);
            }
        }

        // Add the last field if there's anything remaining
        if (current.Length > 0 || input.Length > 0)
        {
            fields.Add(current.ToString().Trim());
        }

        return [.. fields];
    }

    /// <summary>Returns the product of all numbers in the array.</summary>
    public static long SafeProduct(params int[] numbers)
    {
        var result = 1L;
        foreach (var n in numbers)
            result *= (long)n;
        return result;
    }
}
