namespace Evaluator.Settings
{
    public sealed record ApplicationSettings
    {
        // Folder path of llama.cpp where the llama-server.exe can be found
        public string LlamaCppPath { get; set; } = "";
        public int ServerPort { get; set; } = 0;
        public string ModelsFolderPath { get; set; } = "";
        public string Host { get; set; } = "127.0.0.1";
        public string CacheTypeK { get; set; } = "q8_0";
        public string CacheTypeV { get; set; } = "q8_0";
        public SamplingDefaults SamplingDefaults { get; set; } = new();
        public ServerDefaults ServerDefaults { get; set; } = new();
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

    public sealed record SamplingDefaults
    {
        public double Temperature { get; set; } = 0.1;
        public int TopK { get; set; } = 20;
        public double TopP { get; set; } = 0.80;
        public double MinP { get; set; } = 0.05;
        public double RepeatPenalty { get; set; } = 1.15;
        public int RepeatLastN { get; set; } = 1024;
    }

    public sealed record ServerDefaults
    {
        public int Parallel { get; set; } = 1;
        public int Prio { get; set; } = 3;
        public string FlashAttn { get; set; } = "on";
        public string KvUnified { get; set; } = "true";
        public string LoadMode { get; set; } = "mmap";
        public string Fit { get; set; } = "off";
        public int CacheReuse { get; set; } = 256;
        public double DraftPMin { get; set; } = 0.7;
        public int LogVerbosity { get; set; } = 3;
        public string Samplers { get; set; } = "penalties;dry;top_k;top_p;min_p;temperature";
        public string ContextShift { get; set; } = "true";
        public string ReasoningPreserve { get; set; } = "true";
        public string Reasoning { get; set; } = "on";
        public int ReasoningBudget { get; set; } = 4096;
        public string ReasoningBudgetMessage { get; set; } = "\"... Considering the limited time by the user, I have to give the solution based on the thinking directly now.\"";
        public int BatchSize { get; set; } = 1024;
        public int UbatchSize { get; set; } = 512;
        public string SpecType { get; set; } = "none";
    }
}
