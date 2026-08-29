namespace Evaluator.Settings
{

    public sealed record ApplicationSettings
    {
        // Folder path of llama.cpp where the llama-server.exe can be found
        public string LlamaCppPath { get; set; } = "";
        public int ServerPort { get; set; } = 0;
        public string ModelsFolderPath { get; set; } = "";
        public List<ModelSettings> Models { get; set; } = [];

        public string Host { get; set; } = "127.0.0.1";
        public string CacheTypeK { get; set; } = "q8_0";
        public string CacheTypeV { get; set; } = "q8_0";
        public SamplingDefaults SamplingDefaults { get; set; } = new();
        public ServerDefaults ServerDefaults { get; set; } = new();
    }

    /// <summary>
    /// App-level editable sampling defaults shared across all models.
    /// </summary>
    public sealed record SamplingDefaults
    {
        public double Temperature { get; set; } = 0.1;
        public int TopK { get; set; } = 20;
        public double TopP { get; set; } = 0.80;
        public double MinP { get; set; } = 0.05;
        public double RepeatPenalty { get; set; } = 1.15;
        public int RepeatLastN { get; set; } = 1024;
    }

    /// <summary>
    /// Readonly server flags shown in the Settings view (not editable via UI).
    /// Defaults match the reference llama-server command.
    /// </summary>
    public sealed record ServerDefaults
    {
        public int Parallel { get; set; } = 1;
        public int Prio { get; set; } = 3;
        public bool FlashAttn { get; set; } = true;
        public bool KvUnified { get; set; } = true;
        public string LoadMode { get; set; } = "mmap";
        public bool Fit { get; set; } = false;
        public int CacheReuse { get; set; } = 256;
        public double DraftPMin { get; set; } = 0.7;
        public int LogVerbosity { get; set; } = 3;
        public string Samplers { get; set; } = "penalties;dry;top_k;top_p;min_p;temperature";
        public bool ContextShift { get; set; } = true;
        public bool ReasoningPreserve { get; set; } = true;
        public bool Reasoning { get; set; } = true;
        public int ReasoningBudget { get; set; } = 4096;
        public string ReasoningBudgetMessage { get; set; } = "... Considering the limited time by the user, I have to give the solution based on the thinking directly now.";
        public int BatchSize { get; set; } = 1024;
        public int UBatchSize { get; set; } = 512;
        public string SpecType { get; set; } = "none";
    }

    public sealed record ModelSettings
    {
        public string Id { get; set; } = "";
        public string GgufFileName { get; set; } = "";
        public int ContextSize { get; set; } = 0;
        public int GpuLayers { get; set; } = 0;
        public int CpuMoE { get; set; }
        public bool Jinja { get; set; }
        public string Alias { get; set; } = "";
    }
}
