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
            var serverManager = new LlamaServerManager();
            var evaluator = new Evaluator(serverManager);
            
            await evaluator.EvaluateAsync(args[0]);
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
}
