using System.Diagnostics;

namespace Evaluator;

internal sealed class Evaluator
{
    private readonly LlamaServerManager _serverManager;

    public Evaluator(LlamaServerManager serverManager)
    {
        _serverManager = serverManager;
    }

    public async Task EvaluateAsync(string modelId, CancellationToken ct = default)
    {
        var configuration = SettingsManager.Instance.Configuration;
        var modelConfig = configuration.Models.FirstOrDefault(m => m.Id == modelId);

        if (modelConfig is null)
        {
            Console.WriteLine($"Model '{modelId}' not found in configuration.");
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            // TODO: Call API with prompt to ask model for fixing the code

            Console.WriteLine($"Model: {modelId}");
            Console.WriteLine($"Configuration: {SettingsManager.Instance.ConfigurationFilePath}");
            Console.WriteLine($"llama.cpp path: {configuration.LlamaCppPath}");
            Console.WriteLine($"Port: {configuration.DefaultPort}");
        }
        finally
        {
            stopwatch.Stop();
            var result = new ModelEvaluation
            {
                EvaluatorVersion = typeof(Evaluator).Assembly.GetName()?.Version?.ToString() ?? "unknown",
                ModelId = modelId,
                Timestamp = DateTime.UtcNow,
                TotalDurationSeconds = (int)stopwatch.Elapsed.TotalSeconds,
                TotalTokensUsed = 0
            };

            Console.WriteLine($"Duration: {result.TotalDurationSeconds}s");
            Console.WriteLine($"Tokens used: {result.TotalTokensUsed}");
        }
    }
}
