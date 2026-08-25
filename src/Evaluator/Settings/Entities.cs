namespace Evaluator.Settings
{
    public sealed record SamplingDefaults(
        double Temperature = 0.1,
        int TopK = 20,
        double TopP = 0.80,
        double MinP = 0.05,
        double RepeatPenalty = 1.15,
        int RepeatLastN = 1024
    );

    public sealed record ServerDefaults(
        int Parallel = 1,
        int Prio = 3,
        string FlashAttn = "on",
        bool KvUnified = true,
        string LoadMode = "mmap",
        string Fit = "off",
        int CacheReuse = 256,
        double DraftPMin = 0.7,
        int LogVerbosity = 3,
        string Samplers = "penalties;dry;top_k;top_p;min_p;temperature",
        bool ContextShift = true,
        bool ReasoningPreserve = true,
        string Reasoning = "on",
        int ReasoningBudget = 4096,
        string ReasoningBudgetMessage = "\"... Considering the limited time by the user, I have to give the solution based on the thinking directly now.\"",
        int BatchSize = 1024,
        int UbatchSize = 512,
        string SpecType = "none"
    );

    public sealed record ApplicationSettings
    {
        // Folder path of llama.cpp where the llama-server.exe can be found
        public string LlamaCppPath { get; set; } = "";
        public int ServerPort { get; set; } = 0;
        public string ModelsFolderPath { get; set; } = "";
        public string Host { get; set; } = "127.0.0.1";
        public string CacheTypeK { get; set; } = "q8_0";
        public string CacheTypeV { get; set; } = "q8_0";
        public SamplingDefaults? SamplingDefaults { get; set; } = new();
        public ServerDefaults? ServerDefaults { get; set; } = new();
        public List<ModelSettings> Models { get; set; } = [];
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