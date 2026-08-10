using Evaluator.Settings;
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
        var settings = SettingsManager.GetSettings();
        var modelConfig = settings.Models.FirstOrDefault(m => m.Id == modelId);

        if (modelConfig is null)
        {
            Console.WriteLine($"Model '{modelId}' not found in configuration.");
            return;
        }

        var stopwatch = Stopwatch.StartNew();

        try
        {
            Console.WriteLine($"Model: {modelId}");
            Console.WriteLine($"Settings file: {SettingsManager.SettingsFilePath}");
            Console.WriteLine($"llama.cpp folder path: {settings.LlamaCppPath}");
            Console.WriteLine($"Port: {settings.ServerPort}");
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
