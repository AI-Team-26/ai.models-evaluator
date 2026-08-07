namespace Evaluator.Core;

/// <summary>
/// Core orchestrator coordinating all evaluation activities.
/// Manages lifecycle of model tests and result aggregation.
/// </summary>
public interface IEvaluationOrchestrator
{
    /// <summary>
    /// Gets the current version string of the evaluator.
    /// </summary>
    string Version { get; }

    /// <summary>
    /// Runs an evaluation session against specified models and test cases.
    /// </summary>
    /// <param name="modelIds">List of model identifiers to evaluate.</param>
    /// <param name="cancellationToken">Cancellation token for long-running operations.</param>
    /// <returns>Awaitable task containing evaluation results.</returns>
    Task<EvaluationResult> RunEvaluation(
        IEnumerable<string> modelIds, 
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Lists all registered models available for evaluation.
    /// </summary>
    /// <returns>List of model metadata objects.</returns>
    List<ModelMetadata> GetRegisteredModels();
}

/// <summary>
/// Result of a single evaluation run.
/// </summary>
public record EvaluationResult
{
    public DateTime Timestamp { get; init; }
    public int TotalTests { get; set; }
    public int PassedTests { get; set; }
    public int FailedTests { get; set; }
    public Dictionary<string, TestCaseResults> ResultsByModel { get; init; } = new();
}

/// <summary>
/// Test case results grouped by individual test names.
/// </summary>
public record TestCaseResults
{
    public bool Success { get; init; }
    public TimeSpan Duration { get; init; }
    public string? ErrorMessage { get; init; }
}

/// <summary>
/// Metadata describing an evaluable model configuration.
/// </summary>
public record ModelMetadata
{
    public string Id { get; init; } = "";
    public string DisplayName { get; init; } = "";
    public string GgufFilePath { get; init; } = "";
    public int ContextSize { get; init; }
    public bool IsConfiguredForSpeculation { get; init; }
}
