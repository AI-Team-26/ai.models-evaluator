using System.Reflection;
using System.Text.Json;
using Evaluator.Settings;
using NUnit.Framework;

namespace EvaluatorSettingsTests;

[TestFixture]
public sealed class ApplicationSettingsTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    [Test]
    public void Defaults_AreSet()
    {
        var s = new ApplicationSettings();

        Assert.That(s.Host, Is.EqualTo("127.0.0.1"));
        Assert.That(s.CacheTypeK, Is.EqualTo("q8_0"));
        Assert.That(s.CacheTypeV, Is.EqualTo("q8_0"));
        Assert.That(s.SamplingDefaults, Is.Not.Null);
        Assert.That(s.ServerDefaults, Is.Not.Null);
        Assert.That(s.Models, Is.Not.Null);
        Assert.That(s.Models, Is.Empty);
    }

    [Test]
    public void SamplingDefaults_HaveExpectedValues()
    {
        var s = new SamplingDefaults();

        Assert.That(s.Temperature, Is.EqualTo(0.1));
        Assert.That(s.TopK, Is.EqualTo(20));
        Assert.That(s.TopP, Is.EqualTo(0.80));
        Assert.That(s.MinP, Is.EqualTo(0.05));
        Assert.That(s.RepeatPenalty, Is.EqualTo(1.15));
        Assert.That(s.RepeatLastN, Is.EqualTo(1024));
    }

    [Test]
    public void ServerDefaults_HaveExpectedValues()
    {
        var d = new ServerDefaults();

        Assert.That(d.Parallel, Is.EqualTo(1));
        Assert.That(d.Prio, Is.EqualTo(3));
        Assert.That(d.FlashAttn, Is.EqualTo("on"));
        Assert.That(d.KvUnified, Is.True);
        Assert.That(d.LoadMode, Is.EqualTo("mmap"));
        Assert.That(d.Fit, Is.EqualTo("off"));
        Assert.That(d.CacheReuse, Is.EqualTo(256));
        Assert.That(d.DraftPMin, Is.EqualTo(0.7));
        Assert.That(d.LogVerbosity, Is.EqualTo(3));
        Assert.That(d.Samplers, Is.EqualTo("penalties;dry;top_k;top_p;min_p;temperature"));
        Assert.That(d.ContextShift, Is.True);
        Assert.That(d.ReasoningPreserve, Is.True);
        Assert.That(d.Reasoning, Is.EqualTo("on"));
        Assert.That(d.ReasoningBudget, Is.EqualTo(4096));
        Assert.That(d.ReasoningBudgetMessage,
            Is.EqualTo("... Considering the limited time by the user, I have to give the solution based on the thinking directly now."));
        Assert.That(d.BatchSize, Is.EqualTo(1024));
        Assert.That(d.UbatchSize, Is.EqualTo(512));
        Assert.That(d.SpecType, Is.EqualTo("none"));
    }

    [Test]
    public void ModelSettings_AliasDefaultsToEmpty()
    {
        var m = new ModelSettings();

        Assert.That(m.Alias, Is.EqualTo(""));
    }

    [Test]
    public void Json_RoundTrip_PreservesAllFields()
    {
        var original = new ApplicationSettings
        {
            LlamaCppPath = "/opt/llama.cpp",
            Host = "0.0.0.0",
            ServerPort = 8080,
            ModelsFolderPath = "/data/models",
            CacheTypeK = "q4_0",
            CacheTypeV = "q8_0",
            SamplingDefaults = new SamplingDefaults
            {
                Temperature = 0.7,
                TopK = 40,
                TopP = 0.95,
                MinP = 0.1,
                RepeatPenalty = 1.2,
                RepeatLastN = 512
            },
            ServerDefaults = new ServerDefaults
            {
                Parallel = 2,
                Prio = 5,
                SpecType = "draft"
            },
            Models =
            {
                new ModelSettings
                {
                    Id = "model-1",
                    GgufFileName = "model-1.gguf",
                    Alias = "my-alias",
                    ContextSize = 8192,
                    GpuLayers = 35,
                    CpuMoE = 4,
                    Jinja = true
                }
            }
        };

        var json = JsonSerializer.Serialize(original, JsonOptions);
        var loaded = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);

        Assert.That(loaded, Is.Not.Null);
        Assert.That(loaded!.Host, Is.EqualTo("0.0.0.0"));
        Assert.That(loaded.ServerPort, Is.EqualTo(8080));
        Assert.That(loaded.CacheTypeK, Is.EqualTo("q4_0"));
        Assert.That(loaded.CacheTypeV, Is.EqualTo("q8_0"));
        Assert.That(loaded.SamplingDefaults, Is.Not.Null);
        Assert.That(loaded.SamplingDefaults!.Temperature, Is.EqualTo(0.7));
        Assert.That(loaded.SamplingDefaults.TopK, Is.EqualTo(40));
        Assert.That(loaded.SamplingDefaults.TopP, Is.EqualTo(0.95));
        Assert.That(loaded.SamplingDefaults.MinP, Is.EqualTo(0.1));
        Assert.That(loaded.SamplingDefaults.RepeatPenalty, Is.EqualTo(1.2));
        Assert.That(loaded.SamplingDefaults.RepeatLastN, Is.EqualTo(512));
        Assert.That(loaded.ServerDefaults, Is.Not.Null);
        Assert.That(loaded.ServerDefaults!.Parallel, Is.EqualTo(2));
        Assert.That(loaded.ServerDefaults.Prio, Is.EqualTo(5));
        Assert.That(loaded.ServerDefaults.SpecType, Is.EqualTo("draft"));
        Assert.That(loaded.Models, Has.Count.EqualTo(1));
        Assert.That(loaded.Models[0].Alias, Is.EqualTo("my-alias"));
        Assert.That(loaded.Models[0].Jinja, Is.True);
    }

    [Test]
    public void Json_OldSettingsFileWithoutNestedRecords_PreservesDefaults()
    {
        // Simulates an old settings file that pre-dates the new fields.
        // SamplingDefaults/ServerDefaults have default initializers, so deserialization
        // leaves them populated with the constructor defaults (not null). This is the
        // expected behavior; SettingsManager.Load() then null-coalesces for back-compat
        // if the JSON deserializer ever returned null (e.g. with custom converters).
        const string oldJson = @"{
            ""llamaCppPath"": ""/opt/llama.cpp"",
            ""serverPort"": 8001,
            ""modelsFolderPath"": ""/data/models"",
            ""models"": []
        }";

        var loaded = JsonSerializer.Deserialize<ApplicationSettings>(oldJson, JsonOptions);

        Assert.That(loaded, Is.Not.Null);
        // The property initializer keeps SamplingDefaults/ServerDefaults non-null
        // even when the JSON does not contain them.
        Assert.That(loaded!.SamplingDefaults, Is.Not.Null);
        Assert.That(loaded.ServerDefaults, Is.Not.Null);
        Assert.That(loaded.SamplingDefaults!.Temperature, Is.EqualTo(0.1));
        // Host and CacheType* keep their property defaults when missing from JSON
        Assert.That(loaded.Host, Is.EqualTo("127.0.0.1"));
        Assert.That(loaded.CacheTypeK, Is.EqualTo("q8_0"));
        Assert.That(loaded.CacheTypeV, Is.EqualTo("q8_0"));
    }
}
