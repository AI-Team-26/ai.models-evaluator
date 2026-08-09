using System.Text.Json;

namespace Evaluator;

public sealed record Settings
{
    public string LlamaCppPath { get; set; } = "";
    public int DefaultPort { get; set; } = 8001;
    public string ModelsFilePath { get; set; } = "/models";
    public List<ModelSettings> Models { get; set; } = [];
}

public sealed record ModelSettings
{
    public string Id { get; set; } = "";
    public string GgufFileName { get; set; } = "";
    public int ContextSize { get; set; } = 2048;
    public int GpuLayers { get; set; } = 1;
    public int CpuMoE { get; set; }
    public bool Jinja { get; set; }
}

public sealed class SettingsManager
{
    private const string ConfigDir = "LlmEvaluator";
    private const string FileName = "Settings.json";

    private readonly object _lock = new();
    private Settings? _settings;

    public static SettingsManager Instance { get; } = new();

    public string SettingsFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ConfigDir,
        FileName);

    private SettingsManager() {}

    /// <summary>
    /// Returns validated settings or throws InvalidOperationException if configuration is incomplete.
    /// </summary>
    public Settings GetSettings()
    {
        lock (_lock)
        {
            return LoadOrCreateAndValidate();
        }
    }

    public void Save(Settings settings)
    {
        lock (_lock)
        {
            var directory = Path.GetDirectoryName(SettingsFilePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(settings, options));
            _settings = settings;
        }
    }

    public void Reload()
    {
        lock (_lock)
        {
            _settings = null;
        }
    }

    /// <summary>
    /// Loads current settings without validation (for interactive editing).
    /// Returns null if file doesn’t exist yet.
    /// </summary>
    public Settings? LoadCurrent()
    {
        lock (_lock)
        {
            if (_settings != null) return _settings;

            var filePath = SettingsFilePath;
            if (!File.Exists(filePath))
                return null;

            try
            {
                var json = File.ReadAllText(filePath);
                var loaded = JsonSerializer.Deserialize<Settings>(json);
                _settings = loaded;
                return loaded;
            }
            catch
            {
                return null;
            }
        }
    }

    private Settings LoadOrCreateAndValidate()
    {
        var filePath = SettingsFilePath;

        if (!File.Exists(filePath))
        {
            CreateDefaultSettings(out var defaults);
            Save(defaults);
            
            ValidateOrThrow(defaults);
        }

        try
        {
            var json = File.ReadAllText(filePath);
            var loaded = JsonSerializer.Deserialize<Settings>(json);

            if (loaded == null)
                throw new InvalidDataException("Empty or corrupt settings file detected");

            return ValidateOrThrow(loaded);
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Corrupt settings file at {filePath}. Delete it to regenerate.\nError: {ex.Message}", ex);
        }
    }

    private static Settings ValidateOrThrow(Settings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.LlamaCppPath))
            throw new InvalidOperationException(
                "Settings Error: llama.cpp server path must be specified in Settings.json\n" +
                "Please edit the settings file and provide the full path to your llama-server executable.");

        foreach (var model in settings.Models)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                throw new InvalidOperationException(
                    "Settings Error: Each model requires an ID field");

            if (string.IsNullOrWhiteSpace(model.GgufFileName))
                throw new InvalidOperationException(
                    $"'Model '{model.Id}' missing GGUF filename");
        }

        return settings;
    }

    private void CreateDefaultSettings(out Settings result)
    {
        var directory = Path.GetDirectoryName(SettingsFilePath)!;
        Directory.CreateDirectory(directory);

        result = new Settings
        {
            LlamaCppPath = "",
            DefaultPort = 8001,
            ModelsFilePath = "/models",
            Models = []
        };
    }
}
