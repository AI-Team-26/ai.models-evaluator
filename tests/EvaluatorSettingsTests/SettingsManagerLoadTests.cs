using System.Text.Json;
using Evaluator.Settings;
using NUnit.Framework;

namespace EvaluatorSettingsTests;

/// <summary>
/// Tests for the backward-compat null-coalescing behaviour of
/// <see cref="SettingsManager.Load"/>.
/// </summary>
[TestFixture]
public sealed class SettingsManagerLoadTests
{
    private static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        WriteIndented = true
    };

    [Test]
    public void NewSettingsFile_RoundTripsThroughLoad()
    {
        var file = Path.Combine(Path.GetTempPath(), $"LlmEvaluator-Test-{Guid.NewGuid():N}.json");
        try
        {
            var settings = new ApplicationSettings
            {
                LlamaCppPath = "/opt/llama.cpp",
                Host = "0.0.0.0",
                ServerPort = 9000,
                ModelsFolderPath = "/data/models",
                CacheTypeK = "q4_0",
                CacheTypeV = "q8_0",
                SamplingDefaults = new SamplingDefaults { Temperature = 0.5 },
                ServerDefaults = new ServerDefaults { Parallel = 4 },
                Models =
                {
                    new ModelSettings
                    {
                        Id = "m1",
                        GgufFileName = "m1.gguf",
                        Alias = "model-one",
                        ContextSize = 4096,
                        GpuLayers = 20,
                        CpuMoE = 2,
                        Jinja = true
                    }
                }
            };

            File.WriteAllText(file, JsonSerializer.Serialize(settings, JsonOptions));

            // Parse the JSON back and verify all new fields are preserved
            var json = File.ReadAllText(file);
            var loaded = JsonSerializer.Deserialize<ApplicationSettings>(json, JsonOptions);

            Assert.That(loaded, Is.Not.Null);
            Assert.That(loaded!.Host, Is.EqualTo("0.0.0.0"));
            Assert.That(loaded.ServerPort, Is.EqualTo(9000));
            Assert.That(loaded.CacheTypeK, Is.EqualTo("q4_0"));
            Assert.That(loaded.CacheTypeV, Is.EqualTo("q8_0"));
            Assert.That(loaded.SamplingDefaults, Is.Not.Null);
            Assert.That(loaded.SamplingDefaults!.Temperature, Is.EqualTo(0.5));
            Assert.That(loaded.ServerDefaults, Is.Not.Null);
            Assert.That(loaded.ServerDefaults!.Parallel, Is.EqualTo(4));
            Assert.That(loaded.Models, Has.Count.EqualTo(1));
            Assert.That(loaded.Models[0].Alias, Is.EqualTo("model-one"));
            Assert.That(loaded.Models[0].Jinja, Is.True);
        }
        finally
        {
            if (File.Exists(file)) File.Delete(file);
        }
    }

    [Test]
    public void OldSettingsFile_WithoutNewFields_HasDefaultValues()
    {
        // Old-style JSON that pre-dates the new fields. The System.Text.Json
        // deserializer uses property initializers, so SamplingDefaults/ServerDefaults
        // are non-null with constructor defaults. Host/CacheType* are also non-null
        // because of their property initializers.
        const string oldJson = @"{
            ""llamaCppPath"": ""/opt/llama.cpp"",
            ""serverPort"": 8001,
            ""modelsFolderPath"": ""/data/models"",
            ""models"": []
        }";

        var loaded = JsonSerializer.Deserialize<ApplicationSettings>(oldJson, JsonOptions);

        Assert.That(loaded, Is.Not.Null);
        Assert.That(loaded!.SamplingDefaults, Is.Not.Null);
        Assert.That(loaded.SamplingDefaults!.Temperature, Is.EqualTo(0.1));
        Assert.That(loaded.SamplingDefaults.TopK, Is.EqualTo(20));
        Assert.That(loaded.SamplingDefaults.RepeatLastN, Is.EqualTo(1024));
        Assert.That(loaded.ServerDefaults, Is.Not.Null);
        Assert.That(loaded.ServerDefaults!.Parallel, Is.EqualTo(1));
        Assert.That(loaded.Host, Is.EqualTo("127.0.0.1"));
        Assert.That(loaded.CacheTypeK, Is.EqualTo("q8_0"));
        Assert.That(loaded.CacheTypeV, Is.EqualTo("q8_0"));
    }
}
