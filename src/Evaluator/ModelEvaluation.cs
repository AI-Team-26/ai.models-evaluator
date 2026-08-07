namespace Evaluator;

public record ModelEvaluation
{
    public string ModelId { get; init; } = "";
    public string LlamaCppVersion { get; init; } = "";
    public string TestCaseVersion { get; init; } = "";
    public DateTime Timestamp { get; init; }
    
    public int GeneralScore { get; set; }
    public int QualityScore { get; set; }
    public int SpeedScore { get; set; }
    public int IntelligenceScore { get; set; }
    
    public List<string> PositiveNotes { get; set; } = [];
    public List<string> NegativeNotes { get; set; } = [];
    
    public Dictionary<string, bool> TestResultsByTestCaseName { get; set; } = [];
}
