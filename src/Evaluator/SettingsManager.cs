using System.Text.Json;

namespace Evaluator;

public sealed record Settings
{
    public string LlamaCppPath { get; set; } = "llama-server";
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

    public Settings Settings => LoadOrCreateAndValidate();

    public string SettingsFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        ConfigDir,
        FileName);

    public static SettingsManager Instance { get; } = new();

    private SettingsManager() {}

    public void Save()
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

            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(_settings!, options));
        }
    }

    public void Reload()
    {
        lock (_lock)
        {
            _settings = null;
            _ = Settings;
        }
    }

    private Settings LoadOrCreateAndValidate()
    {
        lock (_lock)
        {
            return _settings ??= InitializeOrReload();
        }
    }

    private Settings InitializeOrReload()
    {
        var filePath = SettingsFilePath;

        if (!File.Exists(filePath))
        {
            Console.WriteLine("⚠️  No settings found. Creating default configuration...");
            CreateDefaultSettings(out var defaults);

            Save();
            Console.WriteLine($"✓ Created: {filePath}");
            Console.WriteLine("  Please configure at least one model before running evaluations.");

            return ValidateOrThrow(defaults);
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
                "llama.cpp server path must be specified in Settings.json");

        foreach (var model in settings.Models)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                throw new InvalidOperationException("Each model requires an ID field");

            if (string.IsNullOrWhiteSpace(model.GgufFileName))
                throw new InvalidOperationException(
                    $"Model '{model.Id}' missing GGUF filename");
        }

        return settings;
    }

    private void CreateDefaultSettings(out Settings result)
    {
        var directory = Path.GetDirectoryName(SettingsFilePath)!;
        Directory.CreateDirectory(directory);

        result = new Settings
        {
            LlamaCppPath = "llama-server",
            DefaultPort = 8001,
            ModelsFilePath = "/models",
            Models = []
        };
    }
}
