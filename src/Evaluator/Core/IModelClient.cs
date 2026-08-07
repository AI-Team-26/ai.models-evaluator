namespace Evaluator.Core;

/// <summary>
/// Client for communicating with the inference server via OpenAI-compatible API.
/// Sends prompts and receives completions.
/// </summary>
public interface IModelClient
{
    /// <summary>
    /// Base URL of the inference server.
    /// </summary>
    Uri ServerUrl { get; }

    /// <summary>
    /// Sends a completion request to the model.
    /// </summary>
    /// <param name="prompt">The input prompt text.</param>
    /// <param name="maxTokens">Maximum tokens in response (optional).</param>
    /// <param name="temperature">Temperature parameter for sampling (default: 0.7).</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The generated completion text.</returns>
    Task<string> CompleteTextAsync(
        string prompt, 
        int? maxTokens = null,
        double temperature = 0.7,
        CancellationToken cancellationToken = default);

    /// <summary>
    /// Sends a chat-style message request.
    /// </summary>
    /// <param name="messages">List of role/message pairs.</param>
    /// <param name="cancellationToken">Cancellation token.</param>
    /// <returns>The assistant's response content.</returns>
    Task<string> ChatCompletionAsync(
        IEnumerable<(string Role, string Content)> messages,
        CancellationToken cancellationToken = default);
}
