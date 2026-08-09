using System;
using System.Collections.Generic;
using System.Text;

namespace Evaluator.Settings
{

    public sealed record ApplicationSettings
    {
        public string LlamaCppPath { get; set; } = "";
        public int DefaultPort { get; set; } = 0;
        public string ModelsFilePath { get; set; } = "";
        public List<ModelSettings> Models { get; set; } = [];
    }

    public sealed record ModelSettings
    {
        public string Id { get; set; } = "";
        public string GgufFileName { get; set; } = "";
        public int ContextSize { get; set; } = 0;
        public int GpuLayers { get; set; } = 1;
        public int CpuMoE { get; set; }
        public bool Jinja { get; set; }
    }
}
