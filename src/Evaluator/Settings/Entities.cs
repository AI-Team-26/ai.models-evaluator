namespace Evaluator.Settings
{

    /// <summary>
    /// Editable sampling parameter defaults applied to every model.
    /// Maps to llama-server sampling CLI flags.
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
    /// Readonly server defaults. Stored in JSON so they are visible and inspectable,
    /// but intentionally not exposed for editing through the TUI yet.
    /// </summary>
    public sealed record ServerDefaults
    {
        public int Parallel { get; set; } = 1;
        public int Prio { get; set; } = 3;
        public string FlashAttn { get; set; } = "on";
        public bool KvUnified { get; set; } = true;
        public string LoadMode { get; set; } = "mmap";
        public string Fit { get; set; } = "off";
        public int CacheReuse { get; set; } = 256;
        public double DraftPMIn { get; set; } = 0.7;
        public int LogVerbosity { get; set; } = 3;
        public string Samplers { get; set; } = "penalties;dry;top_k;top_p;min_p;temperature";
        public bool ContextShift { get; set; } = true;
        public bool ReasoningPreserve { get; set; } = true;
        public string Reasoning { get; set; } = "on";
        public int ReasoningBudget { get; set; } = 4096;
        public string ReasoningBudgetMessage { get; set; } = "... Considering the limited time by the user, I have to give the solution based on the thinking directly now.";
        public int BatchSize { get; set; } = 1024;
        public int UBatchSize { get; set; } = 512;
        public string SpecType { get; set; } = "none";
    }

    public sealed record ApplicationSettings
    {
        // Folder path of llama.cpp where the llama-server.exe can be found
        public string LlamaCppPath { get; set; } = "";
        public int ServerPort { get; set; } = 0;
        public string ModelsFolderPath { get; set; } = "";
        public List<ModelSettings> Models { get; set; } = [];

        // App-level editable server flags (llama-server CLI)
        public string Host { get; set; } = "127.0.0.1";
        public string CacheTypeK { get; set; } = "q8_0";
        public string CacheTypeV { get; set; } = "q8_0";

        // App-level editable sampling defaults, shared across all models
        public SamplingDefaults SamplingDefaults { get; set; } = new();

        // Readonly server defaults — shown in the Settings view but not editable via UI
        public ServerDefaults ServerDefaults { get; set; } = new();
    }

    public sealed record ModelSettings
    {
        public string Id { get; set; } = "";
        public string GgufFileName { get; set; } = "";
        public int ContextSize { get; set; } = 0;
        public int GpuLayers { get; set; } = 0;
        public int CpuMoE { get; set; }
        public bool Jinja { get; set; }
        // Optional alias for llama-server --alias. When empty it is auto-generated from the GGUF filename.
        public string Alias { get; set; } = "";
    }
}
