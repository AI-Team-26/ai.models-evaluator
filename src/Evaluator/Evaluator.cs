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
        
        Console.WriteLine($"Model: {modelId}");
        Console.WriteLine($"Configuration: {_configurationPath}");
        Console.WriteLine($"llama.cpp path: {configuration.LlamaCppPath}");
        Console.WriteLine();
        
        // TODO: Implement evaluation process
        // 1. Create git branch with naming convention
        // 2. Start llama-server with model configuration
        // 3. Send buggy code to model via OpenAI-compatible endpoint
        // 4. Apply model's suggested fix
        // 5. Run test suite
        // 6. Log results
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
