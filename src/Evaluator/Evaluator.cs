namespace Evaluator;

internal sealed class Evaluator
{
    private readonly LlamaServerManager _serverManager;
    
    public Evaluator(LlamaServerManager serverManager)
    {
        _serverManager = serverManager;
    }
    
    public async Task<ModelEvaluation> EvaluateAsync(string modelId, CancellationToken ct = default)
    {
        await _serverManager.StartAsync(modelId, ct);
        
        var testCases = GetTestCases();
        var results = new Dictionary<string, bool>();
        var positiveNotes = new List<string>();
        var negativeNotes = new List<string>();
        
        foreach (var testCaseName in testCases)
        {
            var success = await RunSingleTest(testCaseName, ct);
            results[testCaseName] = success;
            
            if (success)
                positiveNotes.Add($"{testCaseName}: Passed");
            else
                negativeNotes.Add($"{testCaseName}: Failed");
        }
        
        var passedCount = results.Count(r => r.Value);
        var totalTests = results.Count;
        
        return new ModelEvaluation
        {
            ModelId = modelId,
            LlamaCppVersion = GetLlamaCppVersion(),
            TestCaseVersion = GitCommitHash(),
            Timestamp = DateTime.UtcNow,
            
            GeneralScore = CalculatePercentage(passedCount, totalTests),
            QualityScore = CalculateQuality(results),
            SpeedScore = 75,
            IntelligenceScore = CalculateIntelligence(negativeNotes.Count),
            
            PositiveNotes = positiveNotes,
            NegativeNotes = negativeNotes,
            TestResultsByTestCaseName = results
        };
    }
    
    private async Task<bool> RunSingleTest(string testName, CancellationToken ct)
    {
        await Task.Delay(100, ct);
        return Random.Shared.NextDouble() > 0.3f;
    }
    
    private static List<string> GetTestCases()
    {
        return
        [
            "BugFix_BasicSyntax",
            "BugFix_NullReference",
            "BugFix_OffByOne"
        ];
    }
    
    private static int CalculatePercentage(int passed, int total) => total == 0 ? 0 : (passed * 100) / total;
    private static int CalculateQuality(Dictionary<string, bool> results) => CalculatePercentage(results.Count(r => r.Value), results.Count);
    private static int CalculateIntelligence(int failedCount) => Math.Max(0, 100 - (failedCount * 10));
    private static string GetLlamaCppVersion() => "v0.4.5";
    private static string GitCommitHash() => "unknown";
}
