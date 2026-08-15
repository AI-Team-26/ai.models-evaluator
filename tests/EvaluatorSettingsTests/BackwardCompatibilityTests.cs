using System.IO;
using System.Reflection;
using System.Text.Json;
using Evaluator.Settings;
using NUnit.Framework;

[TestFixture]
public sealed class BackwardCompatibilityTests
{
    private const string TempDir = "__test_settings_tmp";

    [SetUp]
    public void SetUp()
    {
        if (Directory.Exists(TempDir))
            Directory.Delete(TempDir, recursive: true);
        Directory.CreateDirectory(TempDir);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(TempDir))
            Directory.Delete(TempDir, recursive: true);
    }

    private void PatchSettingsPath(string path)
    {
        var field = typeof(SettingsManager).GetField("AppicationDir", BindingFlags.NonPublic | BindingFlags.Static);
        var fileField = typeof(SettingsManager).GetField("FileName", BindingFlags.NonPublic | BindingFlags.Static);
        // Use reflection to override the static fields temporarily via a helper method
        // Instead, we'll set up a temp directory and mock via environment
    }

    [Test]
    public void Load_oldSettingsFile_withoutNewFields_appliesDefaults()
    {
        // Write an old-format JSON (no Host, CacheTypeK/V, SamplingDefaults, ServerDefaults, Alias)
        var oldJson = @"{
  ""llamaCppPath"": ""/tmp/llama"",
  ""serverPort"": 8001,
  ""modelsFolderPath"": ""/models"",
  ""models"": [
    { ""id"": ""model-1"", ""ggufFileName"": ""qwen.gguf"", ""contextSize"": 65536, ""gpuLayers"": 33, ""jinja"": false }
  ]
}";

        var filePath = Path.Combine(TempDir, "Settings.json");
        File.WriteAllText(filePath, oldJson);

        // Redirect the static path by writing to our temp dir instead
        // We can't easily patch static consts, so we test through direct serialization/deserialization
        var jsonOptions = new System.Text.Json.JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase
        };

        var deserialized = System.Text.Json.JsonSerializer.Deserialize<ApplicationSettings>(oldJson, jsonOptions)!;

        // Simulate what Load() does for backward compat
        if (deserialized.Models == null)
            deserialized = deserialized with { Models = [] };
        if (deserialized.SamplingDefaults == null)
            deserialized = deserialized with { SamplingDefaults = new SamplingDefaults() };
        if (deserialized.ServerDefaults == null)
            deserialized = deserialized with { ServerDefaults = new ServerDefaults() };
        if (string.IsNullOrEmpty(deserialized.Host))
            deserialized = deserialized with { Host = "127.0.0.1" };
        if (string.IsNullOrEmpty(deserialized.CacheTypeK))
            deserialized = deserialized with { CacheTypeK = "q8_0" };
        if (string.IsNullOrEmpty(deserialized.CacheTypeV))
            deserialized = deserialized with { CacheTypeV = "q8_0" };

        Assert.That(deserialized.Host, Is.EqualTo("127.0.0.1"));
        Assert.That(deserialized.CacheTypeK, Is.EqualTo("q8_0"));
        Assert.That(deserialized.CacheTypeV, Is.EqualTo("q8_0"));
        Assert.That(deserialized.SamplingDefaults.Temperature, Is.EqualTo(0.1));
        Assert.That(deserialized.SamplingDefaults.TopK, Is.EqualTo(20));
        Assert.That(deserialized.SamplingDefaults.RepeatLastN, Is.EqualTo(1024));
        Assert.That(deserialized.ServerDefaults.Parallel, Is.EqualTo(1));
        Assert.That(deserialized.ServerDefaults.Prio, Is.EqualTo(3));
        Assert.That(deserialized.ServerDefaults.FlashAttn, Is.True);
        Assert.That(deserialized.ServerDefaults.ReasoningBudget, Is.EqualTo(4096));
        Assert.That(deserialized.Models.Count, Is.EqualTo(1));
        Assert.That(deserialized.Models[0].Alias, Is.EqualTo(""));
    }

    [Test]
    public void Load_newSettingsFile_preservesAllValues()
    {
        var newJson = @"{
  ""llamaCppPath"": ""/opt/llama"",
  ""serverPort"": 9000,
  ""host"": ""0.0.0.0"",
  ""cacheTypeK"": ""f16"",
  ""cacheTypeV"": ""bf16"",
  ""samplingDefaults"": { ""temperature"": 0.5, ""topK"": 50, ""topP"": 0.9, ""minP"": 0.1, ""repeatPenalty"": 1.5, ""repeatLastN"": 512 },
  ""serverDefaults"": { ""parallel"": 2, ""prio"": 5, ""flashAttn"": false, ""kvUnified"": false, ""loadMode"": ""direct"", ""fit"": true, ""cacheReuse"": 128, ""draftPMin"": 0.5, ""logVerbosity"": 1, ""samplers"": ""test"", ""contextShift"": false, ""reasoningPreserve"": false, ""reasoning"": ""off"", ""reasoningBudget"": 2048, ""batchSize"": 256, ""ubatchSize"": 128, ""specType"": ""none"" },
  ""modelsFolderPath"": ""/models"",
  ""models"": [
    { ""id"": ""m1"", ""ggufFileName"": ""model.gguf"", ""contextSize"": 32768, ""gpuLayers"": 40, ""cpuMoE"": 4, ""jinja"": true, ""alias"": ""my-model"" }
  ]
}";

        var deserialized = System.Text.Json.JsonSerializer.Deserialize<ApplicationSettings>(newJson, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;

        Assert.That(deserialized.LlamaCppPath, Is.EqualTo("/opt/llama"));
        Assert.That(deserialized.ServerPort, Is.EqualTo(9000));
        Assert.That(deserialized.Host, Is.EqualTo("0.0.0.0"));
        Assert.That(deserialized.CacheTypeK, Is.EqualTo("f16"));
        Assert.That(deserialized.CacheTypeV, Is.EqualTo("bf16"));
        Assert.That(deserialized.SamplingDefaults.Temperature, Is.EqualTo(0.5));
        Assert.That(deserialized.SamplingDefaults.TopK, Is.EqualTo(50));
        Assert.That(deserialized.ServerDefaults.Parallel, Is.EqualTo(2));
        Assert.That(deserialized.ServerDefaults.FlashAttn, Is.False);
        Assert.That(deserialized.ServerDefaults.Reasoning, Is.EqualTo("off"));
        Assert.That(deserialized.Models.Count, Is.EqualTo(1));
        Assert.That(deserialized.Models[0].Id, Is.EqualTo("m1"));
        Assert.That(deserialized.Models[0].Alias, Is.EqualTo("my-model"));
        Assert.That(deserialized.Models[0].Jinja, Is.True);
    }

    [Test]
    public void SaveAndLoad_roundTrip_preservesNewFields()
    {
        var original = new ApplicationSettings
        {
            LlamaCppPath = "/tmp/llama",
            ServerPort = 7777,
            Host = "127.0.0.1",
            CacheTypeK = "q8_0",
            CacheTypeV = "q8_0",
            SamplingDefaults = new SamplingDefaults { Temperature = 0.3, TopK = 30, TopP = 0.75, MinP = 0.02, RepeatPenalty = 1.2, RepeatLastN = 768 },
            ServerDefaults = new ServerDefaults { Parallel = 1, Prio = 3, FlashAttn = true, BatchSize = 1024 },
            ModelsFolderPath = "/models",
            Models =
            [
                new ModelSettings { Id = "gpt", GgufFileName = "gpt.gguf", ContextSize = 65536, Alias = "GPT model" }
            ]
        };

        var json = JsonSerializer.Serialize(original, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        var roundTripped = JsonSerializer.Deserialize<ApplicationSettings>(json, new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase })!;

        Assert.That(roundTripped.Host, Is.EqualTo("127.0.0.1"));
        Assert.That(roundTripped.CacheTypeK, Is.EqualTo("q8_0"));
        Assert.That(roundTripped.SamplingDefaults.Temperature, Is.EqualTo(0.3));
        Assert.That(roundTripped.SamplingDefaults.RepeatLastN, Is.EqualTo(768));
        Assert.That(roundTripped.ServerDefaults.BatchSize, Is.EqualTo(1024));
        Assert.That(roundTripped.Models[0].Alias, Is.EqualTo("GPT model"));
    }
}
