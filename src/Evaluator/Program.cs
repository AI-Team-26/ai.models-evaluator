namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var serverManager = new LlamaServerManager();
        var evaluator = new Evaluator(serverManager);

        while (true)
        {
            Console.WriteLine();
            Console.WriteLine("=== AI Model Evaluator ===");
            Console.WriteLine("1. See Results");
            Console.WriteLine("2. Run Evaluation");
            Console.WriteLine("3. Change Settings");
            Console.WriteLine("0. Exit");
            Console.Write("Select option: ");

            var choice = Console.ReadLine()?.Trim();

            switch (choice)
            {
                case "1":
                    await ShowResultsAsync();
                    break;
                case "2":
                    await RunEvaluationAsync(evaluator);
                    break;
                case "3":
                    ChangeSettings();
                    break;
                case "0":
                    return 0;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }
        }
    }

    private static async Task ShowResultsAsync()
    {
        var resultsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "results");
        if (!Directory.Exists(resultsDir))
        {
            Console.WriteLine("No results directory found.");
            return;
        }

        var files = Directory.GetFiles(resultsDir, "evaluation_*.json");
        if (files.Length == 0)
        {
            Console.WriteLine("No results found.");
            return;
        }

        foreach (var file in files.OrderByDescending(f => f))
        {
            Console.WriteLine(Path.GetFileName(file));
        }
    }

    private static async Task RunEvaluationAsync(Evaluator evaluator)
    {
        Console.Write("Enter model ID: ");
        var modelId = Console.ReadLine()?.Trim();

        if (string.IsNullOrEmpty(modelId))
        {
            Console.WriteLine("Model ID cannot be empty.");
            return;
        }

        try
        {
            await evaluator.EvaluateAsync(modelId);
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Error: {ex.Message}");
            Console.ResetColor();
        }
    }

    private static void ChangeSettings()
    {
        // TODO: Implement settings changes (configuration path, etc.)
        Console.WriteLine("Settings editor not yet implemented.");
    }
}
