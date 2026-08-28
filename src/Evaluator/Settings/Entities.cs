namespace Evaluator.Settings;

public sealed record SamplingDefaults
{
    public double Temperature { get; init; } = 0.1;
    public int TopK { get; init; } = 20;
    public double TopP { get; init; } = 0.80;
    public double MinP { get; init; } = 0.05;
    public double RepeatPenalty { get; init; } = 1.15;
    public int RepeatLastN { get; init; } = 1024;
}

public sealed record ServerDefaults
{
    public int Parallel { get; init; } = 1;
    public int Prio { get; init; } = 3;
    public string FlashAttn { get; init; } = "on";
    public bool KvUnified { get; init; } = true;
    public string LoadMode { get; init; } = "mmap";
    public string Fit { get; init; } = "off";
    public int CacheReuse { get; init; } = 256;
    public double DraftPMin { get; init; } = 0.7;
    public int LogVerbosity { get; init; } = 3;
    public string Samplers { get; init; } = "penalties;dry;top_k;top_p;min_p;temperature";
    public bool ContextShift { get; init; } = true;
    public bool ReasoningPreserve { get; init; } = true;
    public string Reasoning { get; init; } = "on";
    public int ReasoningBudget { get; init; } = 4096;
    public string ReasoningBudgetMessage { get; init; } =
        "... Considering the limited time by the user, I have to give the solution based on the thinking directly now.";
    public int BatchSize { get; init; } = 1024;
    public int UbatchSize { get; init; } = 512;
    public string SpecType { get; init; } = "none";
}

public sealed record ApplicationSettings
{
    // Folder path of llama.cpp where the llama-server.exe can be found
    public string LlamaCppPath { get; set; } = "";
    public string Host { get; set; } = "127.0.0.1";
    public int ServerPort { get; set; } = 0;
    public string ModelsFolderPath { get; set; } = "";
    public string CacheTypeK { get; set; } = "q8_0";
    public string CacheTypeV { get; set; } = "q8_0";
    public SamplingDefaults SamplingDefaults { get; set; } = new();
    public ServerDefaults ServerDefaults { get; set; } = new();
    public List<ModelSettings> Models { get; set; } = [];
}

public sealed record ModelSettings
{
    public string Id { get; set; } = "";
    public string Alias { get; set; } = "";
    public string GgufFileName { get; set; } = "";
    public int ContextSize { get; set; } = 0;
    public int GpuLayers { get; set; } = 0;
    public int CpuMoE { get; set; }
    public bool Jinja { get; set; }
}
