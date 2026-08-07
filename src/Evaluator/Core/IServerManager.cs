using System.Diagnostics;

namespace Evaluator.Core;

/// <summary>
/// Manages llama.cpp server process lifecycle.
/// Handles startup, health monitoring, and shutdown.
/// </summary>
public interface IServerManager : IDisposable
{
    /// <summary>
    /// Indicates whether a server is currently running.
    /// </summary>
    bool IsRunning { get; }

    /// <summary>
    /// The port number the server is listening on.
    /// </summary>
    int Port { get; }

    /// <summary>
    /// Starts the inference server with given model configuration.
    /// Waits until health check confirms readiness.
    /// </summary>
    /// <param name="modelId">Identifier of the model to load.</param>
    /// <param name="cancellationToken">Token to cancel startup sequence.</param>
    /// <exception cref="InvalidOperationException">Thrown when server fails to start or become ready.</exception>
    Task StartServerAsync(string modelId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Stops the running server gracefully.
    /// </summary>
    /// <param name="force">If true, forcefully terminates the process.</param>
    void StopServer(bool force = false);

    /// <summary>
    /// Performs a health check against the server endpoint.
    /// </summary>
    /// <returns>True if server responds successfully.</returns>
    Task<bool> HealthCheckAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Captures stdout/stderr output from the server process.
    /// </summary>
    event EventHandler<ServerLogEventArgs>? OnLogReceived;
}

/// <summary>
/// Event arguments for server log messages.
/// </summary>
public class ServerLogEventArgs : EventArgs
{
    public LogType Type { get; init; }
    public string Message { get; init; } = "";
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
}

/// <summary>
/// Categorizes different types of log entries.
/// </summary>
public enum LogType
{
    Info,
    Warning,
    Error,
    Debug
}
