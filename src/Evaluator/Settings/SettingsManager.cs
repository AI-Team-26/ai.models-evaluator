using System.Text.Json;

namespace Evaluator.Settings;

public static class SettingsManager
{
    private const string AppicationDir = "LlmEvaluator";
    private const string FileName = "Settings.json";

    //private readonly Lock _lock = new();
    private static ApplicationSettings? settings;
    private static JsonSerializerOptions jsonOptions = new()
    {
        WriteIndented = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static bool HasSettings => settings != null || FileExists();

    public static string SettingsFilePath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
        AppicationDir,
        FileName);

    public static bool FileExists() => File.Exists(SettingsFilePath);

    /// <summary>
    /// Returns validated settings or throws Exception if configuration is incomplete.
    /// </summary>
    public static ApplicationSettings GetSettings(bool forceReload=false)
    {
        if (settings == null || forceReload) {
            if (!FileExists())
                throw new Exception("Settings file not found.");

            return Load();
        }

        return settings ?? throw new Exception("Settings is null"); // in theory settings in never null here
    }

    public static void Save(ApplicationSettings newSettings)
    {
        try
        {
            //lock (@lock)
            {
                var directory = Path.GetDirectoryName(SettingsFilePath);
                if (!string.IsNullOrEmpty(directory))
                    Directory.CreateDirectory(directory);

                File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(newSettings, jsonOptions));
                settings = newSettings;
            }
        }
        catch (Exception exc)
        {
            throw new Exception("Failed to save Settings.", exc);
        }
    }


    /// <summary>
    /// Loads current settings without validation (for interactive editing).
    /// Returns null if file doesn’t exist yet.
    /// </summary>
    /*
    public ApplicationSettings? LoadCurrent()
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
                var loaded = JsonSerializer.Deserialize<ApplicationSettings>(json);
                _settings = loaded;
                return loaded;
            }
            catch
            {
                return null;
            }
        }
    }
    */

    /*
    private ApplicationSettings LoadOrCreateAndValidate()
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
            var loaded = JsonSerializer.Deserialize<ApplicationSettings>(json);

            if (loaded == null)
                throw new InvalidOperationException($"Settings file at {filePath} is empty or corrupt.");

            return ValidateOrThrow(loaded);
        }
        catch (FileNotFoundException)
        {
            throw new InvalidOperationException($"Settings file not found at {filePath}. Starting Settings Editor...");
        }
        catch (JsonException ex)
        {
            throw new InvalidOperationException(
                $"Corrupt settings file at {filePath}. Delete it to regenerate.\nError: {ex.Message}", ex);
        }
    }
    */

    private static ApplicationSettings Load()
    {
        var filePath = SettingsFilePath;

        try
        {
            var json = File.ReadAllText(filePath);
            settings = JsonSerializer.Deserialize<ApplicationSettings>(json)
                ?? throw new Exception($"Settings file at {filePath} is empty or corrupt.");

            return settings;
            //return ValidateOrThrow(settings);
        }
        catch (FileNotFoundException)
        {
            throw new Exception($"Settings file not found at \"{filePath}\".");
        }
        catch (JsonException ex)
        {
            throw new Exception(
                $"Corrupt settings file at {filePath}. Delete it to regenerate.\nError: {ex.Message}", ex);
        }
        catch (Exception exc)
        {
            throw new Exception($"Failed to load Settings. {exc.Message}", exc);
        }
    }

    private static ApplicationSettings ValidateOrThrow(ApplicationSettings settings)
    {
        if (string.IsNullOrWhiteSpace(settings.LlamaCppPath))
            throw new Exception("llama.cpp folder path must be specified.");

        foreach (var model in settings.Models)
        {
            if (string.IsNullOrWhiteSpace(model.Id))
                throw new Exception("Each model requires an ID field.");

            if (string.IsNullOrWhiteSpace(model.GgufFileName))
                throw new Exception($"Model \"{model.Id}\" missing GGUF filename.");
        }

        return settings;
    }

    

    /*
    private void CreateDefaultSettings(out ApplicationSettings result)
    {
        var directory = Path.GetDirectoryName(SettingsFilePath)!;
        Directory.CreateDirectory(directory);

        result = new ApplicationSettings
        {
            LlamaCppPath = "",
            DefaultPort = 8001,
            ModelsFilePath = "/models",
            Models = []
        };
    }*/
}