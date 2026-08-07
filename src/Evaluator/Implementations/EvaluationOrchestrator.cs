using System.Diagnostics;
using Evaluator.Core;

namespace Evaluator.Implementations;

/// <summary>
/// Default implementation of the evaluation orchestrator.
/// Coordinates test execution across models and aggregates results.
/// Currently returns version info only; full logic coming later.
/// </summary>
internal sealed class EvaluationOrchestrator : IEvaluationOrchestrator
{
    private readonly IServerManager _serverManager;
    
    public string Version => GetVersion();

    public EvaluationOrchestrator(IServerManager serverManager)
    {
        _serverManager = serverManager;
    }

    /// <inheritdoc/>
    public async Task<EvaluationResult> RunEvaluation(
        IEnumerable<string> modelIds, 
        CancellationToken cancellationToken = default)
    {
        var result = new EvaluationResult()
        {
            Timestamp = DateTime.UtcNow,
        };

        foreach (var modelId in modelIds)
        {
            await Task.Delay(10, cancellationToken); // Simulate work
            
            result.ResultsByModel[modelId] = new TestCaseResults
            {
                Success = true,
                Duration = TimeSpan.Zero
            };
            
            result.TotalTests++;
            result.PassedTests++;
        }

        return result;
    }

    /// <inheritdoc/>
    public List<ModelMetadata> GetRegisteredModels()
    {
        // TODO(feat/04_config_schema): Load from config/models.json
        return [];
    }

    private static string GetVersion()
    {
        try
        {
            var assembly = typeof(EvaluationOrchestrator).Assembly;
            var versionInfo = FileVersionInfo.GetVersionInfo(assembly.Location);
            return $"{versionInfo.ProductVersion ?? "0.0.0"}-dev";
        }
        catch
        {
            return "0.0.0-dev";
        }
    }
}
