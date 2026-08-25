namespace Evaluator.Settings
{

    public sealed record ApplicationSettings
    {
        // Folder path of llama.cpp where the llama-server.exe can be found
        public string LlamaCppPath { get; set; } = "";
        public int ServerPort { get; set; } = 0;
        public string ModelsFolderPath { get; set; } = "";
        public List<ModelSettings> Models { get; set; } = [];

        // --host
        public string Host { get; set; } = "127.0.0.1";
        // --cache-type-k / --cache-type-v
        public string CacheTypeK { get; set; } = "q8_0";
        public string CacheTypeV { get; set; } = "q8_0";

        // App-level editable sampling defaults, shared across all models
        public SamplingDefaults SamplingDefaults { get; set; } = new();
        // App-level readonly server defaults (shown in Settings view, not editable via UI)
        public ServerDefaults ServerDefaults { get; set; } = new();
    }

    /// <summary>
    /// Editable app-level defaults for llama-server sampling flags.
    /// </summary>
    public sealed record SamplingDefaults
    {
        public double Temperature { get; set; } = 0.1;   // --temperature
        public int TopK { get; set; } = 20;              // --top-k
        public double TopP { get; set; } = 0.80;         // --top-p
        public double MinP { get; set; } = 0.05;         // --min-p
        public double RepeatPenalty { get; set; } = 1.15;// --repeat-penalty
        public int RepeatLastN { get; set; } = 1024;     // --repeat-last-n
    }

    /// <summary>
    /// Readonly app-level defaults for llama-server flags.
    /// Stored in JSON so they are visible/editable by hand, but not exposed in the UI editor.
    /// </summary>
    public sealed record ServerDefaults
    {
        public int Parallel { get; set; } = 1;                                   // --parallel
        public int Prio { get; set; } = 3;                                       // --prio
        public string FlashAttn { get; set; } = "on";                            // --flash-attn
        public bool KvUnified { get; set; } = true;                              // --kv-unified
        public string LoadMode { get; set; } = "mmap";                           // --load-mode
        public string Fit { get; set; } = "off";                                 // --fit
        public int CacheReuse { get; set; } = 256;                               // --cache-reuse
        public double DraftPMIn { get; set; } = 0.7;                             // --draft-p-min
        public int LogVerbosity { get; set; } = 3;                               // --log-verbosity
        public string Samplers { get; set; } = "penalties;dry;top_k;top_p;min_p;temperature"; // --samplers
        public bool ContextShift { get; set; } = true;                           // --context-shift
        public bool ReasoningPreserve { get; set; } = true;                      // --reasoning-preserve
        public string Reasoning { get; set; } = "on";                            // --reasoning
        public int ReasoningBudget { get; set; } = 4096;                         // --reasoning-budget
        public string ReasoningBudgetMessage { get; set; } = "\"... Considering the limited time by the user, I have to give the solution based on the thinking directly now.\""; // --reasoning-budget-message
        public int BatchSize { get; set; } = 1024;                               // --batch-size
        public int UBatchSize { get; set; } = 512;                               // --ubatch-size
        public string SpecType { get; set; } = "none";                           // --spec-type
    }

    public sealed record ModelSettings
    {
        public string Id { get; set; } = "";
        public string GgufFileName { get; set; } = "";
        public int ContextSize { get; set; } = 0;
        public int GpuLayers { get; set; } = 0;
        public int CpuMoE { get; set; }
        public bool Jinja { get; set; }
        // --alias: if empty, auto-generated from GGUF filename (strip .gguf)
        public string Alias { get; set; } = "";
    }
}
