using System.IO;
using System.Reflection;
using Evaluator.Settings;
using NUnit.Framework;

[TestFixture]
public sealed class BackwardCompatibilityTests
{
    private static readonly string TempDir = Path.Combine(Path.GetTempPath(), "EvaluatorSettingsTests");
    private const string OriginalAppicationDir = "LlmEvaluator";
    private const string OriginalFileName = "Settings.json";

    [SetUp]
    public void SetUp()
    {
        // Clean up temp dir from previous run
        if (Directory.Exists(TempDir))
            Directory.Delete(TempDir, recursive: true);
        Directory.CreateDirectory(TempDir);
        // Reset cached settings from previous test
        var field = typeof(SettingsManager).GetField("settings", BindingFlags.NonPublic | BindingFlags.Static)!;
        field.SetValue(null, null);
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(TempDir))
            Directory.Delete(TempDir, recursive: true);
        // Always reset cached settings
        var field = typeof(SettingsManager).GetField("settings", BindingFlags.NonPublic | BindingFlags.Static)!;
        field.SetValue(null, null);
    }

    private void OverrideSettingsPath()
    {
        var dirField = typeof(SettingsManager).GetField("AppicationDir", BindingFlags.NonPublic | BindingFlags.Static)!;
        var fileField = typeof(SettingsManager).GetField("FileName", BindingFlags.NonPublic | BindingFlags.Static)!;
        dirField.SetValue(null, TempDir);
        fileField.SetValue(null, "Settings.json");
    }

    private string ComputeFullTempPath() => Path.Combine(TempDir, "Settings.json");

    private void RestoreSettingsPath()
    {
        var dirField = typeof(SettingsManager).GetField("AppicationDir", BindingFlags.NonPublic | BindingFlags.Static)!;
        var fileField = typeof(SettingsManager).GetField("FileName", BindingFlags.NonPublic | BindingFlags.Static)!;
        dirField.SetValue(null, OriginalAppicationDir);
        fileField.SetValue(null, OriginalFileName);
        var field = typeof(SettingsManager).GetField("settings", BindingFlags.NonPublic | BindingFlags.Static)!;
        field.SetValue(null, null);
    }

    [Test]
    public void Load_oldSettingsFile_withoutNewFields_appliesDefaults()
    {
        var oldJson = @"{
  ""llamaCppPath"": ""/tmp/llama"",
  ""serverPort"": 8001,
  ""modelsFolderPath"": ""/models"",
  ""models"": [
    { ""id"": ""model-1"", ""ggufFileName"": ""qwen.gguf"", ""contextSize"": 65536, ""gpuLayers"": 33, ""jinja"": false }
  ]
}";

        File.WriteAllText(ComputeFullTempPath(), oldJson);

        OverrideSettingsPath();

        try
        {
            var loaded = SettingsManager.GetSettings(forceReload: true);

            Assert.That(loaded.Host, Is.EqualTo("127.0.0.1"));
            Assert.That(loaded.CacheTypeK, Is.EqualTo("q8_0"));
            Assert.That(loaded.CacheTypeV, Is.EqualTo("q8_0"));
            Assert.That(loaded.SamplingDefaults.Temperature, Is.EqualTo(0.1));
            Assert.That(loaded.SamplingDefaults.TopK, Is.EqualTo(20));
            Assert.That(loaded.SamplingDefaults.RepeatLastN, Is.EqualTo(1024));
            Assert.That(loaded.ServerDefaults.Parallel, Is.EqualTo(1));
            Assert.That(loaded.ServerDefaults.Prio, Is.EqualTo(3));
            Assert.That(loaded.ServerDefaults.FlashAttn, Is.True);
            Assert.That(loaded.ServerDefaults.ReasoningBudget, Is.EqualTo(4096));
            Assert.That(loaded.Models.Count, Is.EqualTo(1));
            Assert.That(loaded.Models[0].Alias, Is.EqualTo(""));
        }
        finally
        {
            RestoreSettingsPath();
        }
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

        File.WriteAllText(ComputeFullTempPath(), newJson);

        OverrideSettingsPath();

        try
        {
            var loaded = SettingsManager.GetSettings(forceReload: true);

            Assert.That(loaded.LlamaCppPath, Is.EqualTo("/opt/llama"));
            Assert.That(loaded.ServerPort, Is.EqualTo(9000));
            Assert.That(loaded.Host, Is.EqualTo("0.0.0.0"));
            Assert.That(loaded.CacheTypeK, Is.EqualTo("f16"));
            Assert.That(loaded.CacheTypeV, Is.EqualTo("bf16"));
            Assert.That(loaded.SamplingDefaults.Temperature, Is.EqualTo(0.5));
            Assert.That(loaded.SamplingDefaults.TopK, Is.EqualTo(50));
            Assert.That(loaded.ServerDefaults.Parallel, Is.EqualTo(2));
            Assert.That(loaded.ServerDefaults.FlashAttn, Is.False);
            Assert.That(loaded.ServerDefaults.Reasoning, Is.EqualTo("off"));
            Assert.That(loaded.Models.Count, Is.EqualTo(1));
            Assert.That(loaded.Models[0].Id, Is.EqualTo("m1"));
            Assert.That(loaded.Models[0].Alias, Is.EqualTo("my-model"));
            Assert.That(loaded.Models[0].Jinja, Is.True);
        }
        finally
        {
            RestoreSettingsPath();
        }
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

        OverrideSettingsPath();

        try
        {
            SettingsManager.Save(original);
            var roundTripped = SettingsManager.GetSettings(forceReload: true);

            Assert.That(roundTripped.Host, Is.EqualTo("127.0.0.1"));
            Assert.That(roundTripped.CacheTypeK, Is.EqualTo("q8_0"));
            Assert.That(roundTripped.SamplingDefaults.Temperature, Is.EqualTo(0.3));
            Assert.That(roundTripped.SamplingDefaults.RepeatLastN, Is.EqualTo(768));
            Assert.That(roundTripped.ServerDefaults.BatchSize, Is.EqualTo(1024));
            Assert.That(roundTripped.Models[0].Alias, Is.EqualTo("GPT model"));
        }
        finally
        {
            RestoreSettingsPath();
        }
    }
}
