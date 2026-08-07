using Evaluator.Core;

namespace Evaluator.Implementations;

/// <summary>
/// Placeholder server manager used during scaffolding phase.
/// Replaced by real Process-based implementation in feat/03_server_management.
/// </summary>
internal sealed class PlaceholderServerManager : IServerManager
{
    public bool IsRunning => false;
    public int Port => 8080;

    public event EventHandler<ServerLogEventArgs>? OnLogReceived;

    public void Dispose()
    {
        StopServer(false);
    }

    public Task<bool> HealthCheckAsync(CancellationToken cancellationToken = default)
    {
        Console.WriteLine("   [Health Check] Placeholder mode - no actual server running");
        return Task.FromResult(true);
    }

    public Task StartServerAsync(string modelId, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"   [Start] Placeholder mode - would start model '{modelId}' on port {_port}");
        
        OnLogReceived?.Invoke(this, new ServerLogEventArgs
        {
            Type = LogType.Info,
            Message = $"Starting placeholder server for model: {modelId}"
        });

        return Task.CompletedTask;
    }

    public void StopServer(bool force = false)
    {
        if (!IsRunning) return;

        Console.WriteLine($"   [Stop] Placeholder server stopped{(force ? " forcefully" : "")}");
        
        OnLogReceived?.Invoke(this, new ServerLogEventArgs
        {
            Type = LogType.Info,
            Message = $"Stopped placeholder server{(force ? " (forced)" : "")}"
        });
    }

    private const int _port = 8080;
}
