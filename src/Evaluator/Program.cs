using Evaluator.Core;
using Evaluator.Implementations;
using Microsoft.Extensions.DependencyInjection;

namespace Evaluator;

/// <summary>
/// Main entry point for the AI Model Evaluator orchestration tool.
/// Coordinates model testing against buggy code samples.
/// </summary>
public static class Program
{
    /// <summary>
    /// Application entry point. Initializes services and displays startup info.
    /// Returns exit code 0 on success, non-zero otherwise.
    /// </summary>
    public static async Task<int> Main(string[] args)
    {
        try
        {
            // Configure dependency injection container
            var services = new ServiceCollection();
            
            RegisterServices(services);

            var serviceProvider = services.BuildServiceProvider();

            // Get evaluation orchestrator and display initialization message
            var orchestrator = serviceProvider.GetRequiredService<IEvaluationOrchestrator>();
            
            Console.WriteLine("🔬 AI Model Evaluator initialized.");
            Console.WriteLine($"   Version: {orchestrator.Version}");
            Console.WriteLine($"   Working Directory: {Directory.GetCurrentDirectory()}");
            Console.WriteLine();
            Console.WriteLine("Ready to evaluate models against target code bugs.");
            Console.WriteLine("(Placeholder mode - server management coming soon)");

            return 0;
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"❌ Error initializing evaluator: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.Error.WriteLine($"   Inner: {ex.InnerException.Message}");
            }
            return 1;
        }
    }

    private static void RegisterServices(IServiceCollection services)
    {
        // Core orchestrator coordinates evaluation workflow
        services.AddSingleton<IEvaluationOrchestrator, EvaluationOrchestrator>();

        // Server manager handles llama.cpp process lifecycle  
        // TODO(feat/03_server_management): Implement real Process-based server control
        services.AddSingleton<IServerManager, PlaceholderServerManager>();

        // Model client communicates with inference server
        // TODO(Post-Core): Implement actual OpenAI-compatible HTTP client
        services.AddScoped<IModelClient, PlaceholderModelClient>();
    }
}
