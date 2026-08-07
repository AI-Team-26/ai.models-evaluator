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
        var serverManager = new LlamaServerManager();
        var evaluator = new Evaluator(serverManager);
        
        Console.WriteLine($"Evaluating '{modelId}'...");
        
        // TODO: Implement evaluation logic using evaluator.Evaluate()
        await Task.Delay(100);
        
        Console.WriteLine("Evaluation complete (placeholder).");
    }
}
