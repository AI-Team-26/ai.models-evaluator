using System.Text.Json.Serialization;

namespace Evaluator.Settings;

public record ApplicationSettings
{
    public string? LlamaCppPath { get; set; } = "";
    public int ServerPort { get; set; } = 0;
    public string? ModelsFolderPath { get; set; } = "";

    [JsonPropertyName("host")]
    public string? Host { get; set; } = "127.0.0.1";

    [JsonPropertyName("cacheTypeK")]
    public string? CacheTypeK { get; set; } = "q8_0";

    [JsonPropertyName("cacheTypeV")]
    public string? CacheTypeV { get; set; } = "q8_0";

    [JsonPropertyName("samplingDefaults")]
    public SamplingDefaults? SamplingDefaults { get; set; }

    [JsonPropertyName("serverDefaults")]
    public ServerDefaults? ServerDefaults { get; set; }

    public List<ModelSettings> Models { get; set; } = [];
}

public record SamplingDefaults
{
    public double Temperature { get; set; }
    public int TopK { get; set; }
    public double TopP { get; set; }
    public double MinP { get; set; }
    public double RepeatPenalty { get; set; }
    public int RepeatLastN { get; set; }
    
    public SamplingDefaults()
    {
        Temperature = 0.1;
        TopK = 20;
        TopP = 0.80;
        MinP = 0.05;
        RepeatPenalty = 1.15;
        RepeatLastN = 1024;
    }
}

public record ServerDefaults
{
    public int Parallel { get; set; }
    public int Prio { get; set; }
    public string? FlashAttn { get; set; }
    public bool KvUnified { get; set; }
    public string? LoadMode { get; set; }
    public string? Fit { get; set; }
    public int CacheReuse { get; set; }
    public double DraftPMin { get; set; }
    public int LogVerbosity { get; set; }
    public string? Samplers { get; set; }
    public bool ContextShift { get; set; }
    public bool ReasoningPreserve { get; set; }
    public string? Reasoning { get; set; }
    public int ReasoningBudget { get; set; }
    public string? ReasoningBudgetMessage { get; set; }
    public int BatchSize { get; set; }
    public int UbatchSize { get; set; }
    public string? SpecType { get; set; }
    
    public ServerDefaults()
    {
        Parallel = 1;
        Prio = 3;
        FlashAttn = "on";
        KvUnified = true;
        LoadMode = "mmap";
        Fit = "off";
        CacheReuse = 256;
        DraftPMin = 0.7;
        LogVerbosity = 3;
        Samplers = "penalties;dry;top_k;top_p;min_p;temperature";
        ContextShift = true;
        ReasoningPreserve = true;
        Reasoning = "on";
        ReasoningBudget = 4096;
        ReasoningBudgetMessage = "... Considering the limited time by the user, I have to give the solution based on the thinking directly now.";
        BatchSize = 1024;
        UbatchSize = 512;
        SpecType = "none";
    }
}

public record ModelSettings
{
    public string Id { get; set; } = "";
    public string GgufFileName { get; set; } = "";
    public int ContextSize { get; set; } = 0;
    public int GpuLayers { get; set; } = 0;
    public int CpuMoE { get; set; } = 0;
    public bool Jinja { get; set; } = false;
    
    [JsonPropertyName("alias")]
    public string? Alias { get; set; } = "";
}