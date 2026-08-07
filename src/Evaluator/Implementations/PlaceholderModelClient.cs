using Evaluator.Core;

namespace Evaluator.Implementations;

/// <summary>
/// Placeholder model client used during scaffolding phase.
/// Returns mock responses until real HTTP client is implemented.
/// </summary>
internal sealed class PlaceholderModelClient : IModelClient
{
    public Uri ServerUrl => new("http://localhost:8080/v1");

    public async Task<string> CompleteTextAsync(
        string prompt, 
        int? maxTokens = null,
        double temperature = 0.7,
        CancellationToken cancellationToken = default)
    {
        await Task.Yield(); // Make this truly async
        
        return $"""
            [PLACEHOLDER RESPONSE]
            Prompt received: "{prompt.Length} chars"
            Max tokens: {(maxTokens.HasValue ? maxTokens.Value.ToString() : "unlimited")}
            Temperature: {temperature:F2}
            
            Real implementation coming in Post-Core step.
            """;
    }

    public async Task<string> ChatCompletionAsync(
        IEnumerable<(string Role, string Content)> messages,
        CancellationToken cancellationToken = default)
    {
        await Task.Yield();

        var messageCount = messages.Count();
        var lastMessage = messages.LastOrDefault().Content ?? "(empty)";

        return $"""
            [CHAT PLACEHOLDER]
            Messages processed: {messageCount}
            Last role: {(messages.LastOrDefault().Role)}
            Content preview: "...{lastMessage[..Math.Min(50, lastMessage.Length)]}..."
            
            OpenAI-compatible chat API will be implemented later.
            """;
    }
}
