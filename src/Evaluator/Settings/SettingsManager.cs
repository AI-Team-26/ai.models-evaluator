using System.Text.Json;

namespace Evaluator.Settings;

public static class SettingsManager
{
    private const string AppicationDir = "LlmEvaluator";
    private const string FileName = "Settings.json";

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
    /// Raise an Exception if Settings cannot be loaded.
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
            var directory = Path.GetDirectoryName(SettingsFilePath);
            if (!string.IsNullOrEmpty(directory))
                Directory.CreateDirectory(directory);

            File.WriteAllText(SettingsFilePath, JsonSerializer.Serialize(newSettings, jsonOptions));
            settings = newSettings;
        }
        catch (Exception exc)
        {
            throw new Exception("Failed to save Settings.", exc);
        }
    }


    private static ApplicationSettings Load()
    {
        var filePath = SettingsFilePath;

        try
        {
            var json = File.ReadAllText(filePath);
            settings = JsonSerializer.Deserialize<ApplicationSettings>(json, jsonOptions)
                ?? throw new Exception($"Settings file at {filePath} is empty or corrupt.");

            // Backward compat: old settings files lack these fields
            settings.SamplingDefaults ??= new SamplingDefaults();
            settings.ServerDefaults ??= new ServerDefaults();
            if (string.IsNullOrWhiteSpace(settings.Host))
                settings.Host = "127.0.0.1";
            if (string.IsNullOrWhiteSpace(settings.CacheTypeK))
                settings.CacheTypeK = "q8_0";
            if (string.IsNullOrWhiteSpace(settings.CacheTypeV))
                settings.CacheTypeV = "q8_0";

            return settings;
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

}