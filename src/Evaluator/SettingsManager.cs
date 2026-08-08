using System.Text.Json;

namespace Evaluator;

/// <summary>
/// Singleton manager for application-wide configuration.
/// Handles loading/saving settings from ~/LlmEvaluator/Configuration.json
/// </summary>
public sealed class SettingsManager
{
    private static readonly Lazy<SettingsManager> _instance = 
        new(() => new SettingsManager());

    public static SettingsManager Instance => _instance.Value;

    private const string DefaultConfigDir = "LlmEvaluator";
    private const string ConfigFileName = "Configuration.json";
    
    private readonly object _lock = new();
    private Configuration? _configuration;

    /// <summary>
    /// Gets the current configuration. Auto-initializes if not loaded.
    /// </summary>
    public Configuration Configuration
    {
        get
        {
            lock (_lock)
            {
                return _configuration ?? LoadOrCreateConfiguration();
            }
        }
    }

    /// <summary>
    /// Full path to the configuration file
    /// </summary>
    public string ConfigurationFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        DefaultConfigDir,
        ConfigFileName);

    // Private constructor for singleton pattern
    private SettingsManager()
    {
        // Intentionally empty - initialization happens lazily via property access
    }

    /// <summary>
    /// Saves current configuration to disk
    /// </summary>
    public void Save()
    {
        lock (_lock)
        {
            var directory = Path.GetDirectoryName(ConfigurationFilePath);
            if (!string.IsNullOrEmpty(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var options = new JsonSerializerOptions 
            { 
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            
            File.WriteAllText(ConfigurationFilePath, 
                JsonSerializer.Serialize(_configuration, options));
        }
    }

    /// <summary>
    /// Reloads configuration from disk (refreshes cached copy)
    /// </summary>
    public void Reload()
    {
        lock (_lock)
        {
            _configuration = null;
            _ = Configuration; // Trigger reload
        }
    }

    /// <summary>
    /// Loads existing configuration or creates default if none exists
    /// </summary>
    private Configuration LoadOrCreateConfiguration()
    {
        if (File.Exists(ConfigurationFilePath))
        {
            try
            {
                var json = File.ReadAllText(ConfigurationFilePath);
                _configuration = JsonSerializer.Deserialize<Configuration>(json);
                
                if (_configuration is not null)
                {
                    Console.WriteLine($"Loaded configuration from {this.ConfigurationFilePath}");
                    return _configuration;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Warning: Failed to load config: {ex.Message}");
            }
        }

        _configuration = CreateDefaultConfiguration()!;
        Save();
        
        Console.WriteLine($"Created default configuration at {ConfigurationFilePath}");
        return _configuration;
    }

    /// <summary>
    /// Creates a fresh default configuration
    /// </summary>
    private static Configuration CreateDefaultConfiguration() => new()
    {
        LlamaCppPath = "llama-server",
        DefaultPort = 8001,
        ModelsFilePath = "/models",
        Models = []
    };
}

/// <summary>
/// Application configuration record
/// </summary>
public sealed record Configuration
{
    public string LlamaCppPath { get; set; } = "llama-server";
    public int DefaultPort { get; set; } = 8001;
    public string ModelsFilePath { get; set; } = "/models";
    public List<ModelSettings> Models { get; set; } = [];
}

/// <summary>
/// Per-model configuration settings
/// </summary>
public sealed record ModelSettings
{
    public string Id { get; set; } = "";
    public string GgufFileName { get; set; } = "";
    public int ContextSize { get; set; } = 2048;
    public int GpuLayers { get; set; } = 1;
    public int CpuMoE { get; set; }
    public bool Jinja { get; set; }
}
