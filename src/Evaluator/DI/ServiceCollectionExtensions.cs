using Microsoft.Extensions.DependencyInjection;
using Evaluator.Core;
using Evaluator.Implementations;

namespace Evaluator;

/// <summary>
/// Extension methods for configuring evaluator services.
/// Uses placeholder implementations during scaffolding phase.
/// Real implementations will be added in subsequent steps.
/// </summary>
public static class ServiceCollectionExtensions
{
    /// <summary>
    /// Registers all core evaluator services into the container.
    /// </summary>
    public static IServiceCollection RegisterEvaluatorServices(this IServiceCollection services)
    {
        // Core orchestrator coordinates evaluation workflow
        services.AddSingleton<IEvaluationOrchestrator, EvaluationOrchestrator>();

        // Server manager handles llama.cpp process lifecycle  
        // TODO(feat/03_server_management): Implement real Process-based server control
        services.AddSingleton<IServerManager, PlaceholderServerManager>();

        // Model client communicates with inference server
        // TODO(Post-Core): Implement actual OpenAI-compatible HTTP client
        services.AddScoped<IModelClient, PlaceholderModelClient>();

        return services;
    }
}
