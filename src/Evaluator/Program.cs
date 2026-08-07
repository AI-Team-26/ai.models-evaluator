using Spectre.Console;

namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        if (args.Length == 0 || args[0].StartsWith("--help") || args[0] == "-h")
        {
            Console.WriteLine("AI Model Evaluator\n");
            Console.WriteLine("Usage: evaluator <model-id>");
            Console.WriteLine("\nExample: evaluator llama-3.1-8b");
            return 0;
        }
        
        try
        {
            await RunEvaluation(args[0]);
            return 0;
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
            return 1;
        }
    }
    
    private static async Task RunEvaluation(string modelId)
    {
        var serverManager = new Core.FakeLlamaServerManager();
        var evaluator = new Core.Evaluator(serverManager);
        
        Console.Write($"Evaluating '{modelId}'... ");
        Console.CursorVisible = false;
        
        var result = await evaluator.EvaluateAsync(modelId);
        
        Console.CursorVisible = true;
        Console.WriteLine("Done!");
        
        DisplayResult(result);
    }
    
    private static void DisplayResult(Domain.ModelEvaluation eval)
    {
        Console.WriteLine();
        
        PrintTableHeader(new[] { "Property", "Value" });
        PrintTableRow("Model ID", eval.ModelId);
        PrintTableRow("llama.cpp Version", eval.LlamaCppVersion);
        PrintTableRow("Test Case Version", eval.TestCaseVersion);
        PrintTableRow("Timestamp", eval.Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
        Console.WriteLine();
        
        Console.WriteLine("Scores:");
        PrintScore("General", eval.GeneralScore);
        PrintScore("Quality", eval.QualityScore);
        PrintScore("Speed", eval.SpeedScore);
        PrintScore("Intelligence", eval.IntelligenceScore);
        Console.WriteLine();
        
        if (eval.PositiveNotes.Any())
        {
            Console.WriteLine("Positive Notes:");
            foreach (var note in eval.PositiveNotes)
                Console.WriteLine($"  + {note}");
            Console.WriteLine();
        }
        
        if (eval.NegativeNotes.Any())
        {
            Console.WriteLine("Negative Notes:");
            foreach (var note in eval.NegativeNotes)
                Console.WriteLine($"  - {note}");
            Console.WriteLine();
        }
        
        Console.WriteLine($"{eval.TestResultsByTestCaseName.Count} test cases evaluated.");
    }
    
    private static void PrintTableHeader(string[] headers)
    {
        var row = string.Join(" | ", headers.Select(h => h.PadRight(headers.Max(x => x.Length))));
        Console.WriteLine(row);
        Console.WriteLine(new string('-', row.Length));
    }
    
    private static void PrintTableRow(params string[] cells)
    {
        var maxLengths = GetMaxColumnLengths(cells);
        var row = string.Join(" | ", cells.Zip(maxLengths).Select(c => c.First!.PadRight(c.Second)));
        Console.WriteLine(row);
    }
    
    private static List<int> GetMaxColumnLengths(IEnumerable<string> values)
    {
        return Enumerable.Repeat(values.Max(v => v?.Length ?? 0), values.Count()).ToList();
    }
    
    private static void PrintScore(string metric, int score)
    {
        SetScoreColor(score);
        Console.WriteLine($"{metric,-15} {score}/100");
        Console.ResetColor();
    }
    
    private static void SetScoreColor(int score)
    {
        if (score >= 80)
            Console.ForegroundColor = ConsoleColor.Green;
        else if (score >= 50)
            Console.ForegroundColor = ConsoleColor.Yellow;
        else
            Console.ForegroundColor = ConsoleColor.Red;
    }
}
