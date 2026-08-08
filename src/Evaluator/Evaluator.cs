using System.Diagnostics;
using System.Text.Json;

namespace Evaluator;

internal sealed class Evaluator
{
    private readonly LlamaServerManager _serverManager;
    private readonly string _configurationPath;

    public Evaluator(LlamaServerManager serverManager)
    {
        _serverManager = serverManager;
        _configurationPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
            "LlmEvaluator",
            "Configuration.json");
    }

    public async Task EvaluateAsync(string modelId, CancellationToken ct = default)
    {
        var configuration = LoadOrCreateConfiguration();
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
            Console.WriteLine($"Configuration: {_configurationPath}");
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

    private Configuration LoadOrCreateConfiguration()
    {
        if (File.Exists(_configurationPath))
        {
            var json = File.ReadAllText(_configurationPath);
            return JsonSerializer.Deserialize<Configuration>(json) ?? CreateDefaultConfiguration();
        }

        var defaultConfig = CreateDefaultConfiguration();
        var directory = Path.GetDirectoryName(_configurationPath);
        if (!string.IsNullOrEmpty(directory))
            Directory.CreateDirectory(directory);

        var options = new JsonSerializerOptions { WriteIndented = true };
        File.WriteAllText(_configurationPath, JsonSerializer.Serialize(defaultConfig, options));

        Console.WriteLine($"Created default configuration at {_configurationPath}");
        return defaultConfig;
    }

    private static Configuration CreateDefaultConfiguration() => new()
    {
        LlamaCppPath = "llama-server",
        DefaultPort = 8001,
        ModelsFilePath = "/models",
        Models = []
    };
}

internal sealed record Configuration
{
    public string LlamaCppPath { get; set; } = "llama-server";
    public int DefaultPort { get; set; } = 8001;
    public string ModelsFilePath { get; set; } = "/models";
    public List<ModelSettings> Models { get; set; } = [];
}

internal sealed record ModelSettings
{
    public string Id { get; set; } = "";
    public string GgufFileName { get; set; } = "";
    public int ContextSize { get; set; } = 2048;
    public int GpuLayers { get; set; } = 1;
    public int CpuMoE { get; set; }
    public bool Jinja { get; set; }
}
