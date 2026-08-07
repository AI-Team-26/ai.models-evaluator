using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;

namespace Evaluator;

internal sealed class Evaluator
{
    private readonly LlamaServerManager _serverManager;
    private readonly string _configurationPath;
    private readonly HttpClient _httpClient;

    public Evaluator(LlamaServerManager serverManager)
    {
        _serverManager = serverManager;
        _httpClient = new HttpClient();
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
        long totalTokens = 0;

        try
        {
            // TODO: 1. Create git branch with naming convention
            // TODO: 2. Start llama-server with model configuration
            // TODO: 3. Send buggy code to model via OpenAI-compatible endpoint
            // TODO: 4. Apply model's suggested fix
            // TODO: 5. Run test suite (dotnet test)
            // TODO: 6. Log results

            Console.WriteLine($"Model: {modelId}");
            Console.WriteLine($"Configuration: {_configurationPath}");
            Console.WriteLine($"llama.cpp path: {configuration.LlamaCppPath}");
            Console.WriteLine($"Port: {configuration.DefaultPort}");

            // Send buggy code to model via OpenAI-compatible endpoint
            var buggyCode = GetBuggyCode();
            var prompt = $"Create a branch and fix the bugs in the TargetCode project.\n\nBuggy code:\n{buggyCode}";
            var response = await CallLlamaApiAsync(prompt, ct);
            totalTokens = response.Usage?.TotalTokens ?? 0;

            // TODO: Apply model's suggested fix
            // TODO: Run test suite (dotnet test)
            // TODO: Log results
        }
        finally
        {
            stopwatch.Stop();
            var result = new ModelEvaluation
            {
                ModelId = modelId,
                Timestamp = DateTime.UtcNow,
                TotalDurationSeconds = (int)stopwatch.Elapsed.TotalSeconds,
                TotalTokensUsed = totalTokens
            };

            Console.WriteLine($"Duration: {result.TotalDurationSeconds}s");
            Console.WriteLine($"Tokens used: {result.TotalTokensUsed}");
        }
    }

    private string GetBuggyCode()
    {
        var targetCodeDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "src", "TargetCode");
        if (Directory.Exists(targetCodeDir))
        {
            var files = Directory.GetFiles(targetCodeDir, "*.cs", SearchOption.AllDirectories);
            return string.Join("\n\n", files.Select(f => $"--- {f} ---\n" + File.ReadAllText(f)));
        }
        return "(no buggy code found)";
    }

    private async Task<LlamaApiResponse> CallLlamaApiAsync(string prompt, CancellationToken ct)
    {
        // TODO: Call llama-server OpenAI-compatible endpoint
        // Endpoint: http://localhost:{port}/v1/chat/completions
        throw new NotImplementedException();
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
        DefaultPort = 8080,
        Models = []
    };
}

internal sealed class LlamaApiResponse
{
    public UsageInfo? Usage { get; set; }
}

internal sealed class UsageInfo
{
    public int TotalTokens { get; set; }
}

internal sealed class Configuration
{
    public string LlamaCppPath { get; set; } = "llama-server";
    public int DefaultPort { get; set; } = 8080;
    public List<ModelSettings> Models { get; set; } = [];
}

internal sealed class ModelSettings
{
    public string Id { get; set; } = "";
    public string GgufFilePath { get; set; } = "";
    public int ContextSize { get; set; } = 2048;
    public int GpuLayers { get; set; } = 1;
    public bool CpuMoe { get; set; }
    public bool Jinja { get; set; }
}
