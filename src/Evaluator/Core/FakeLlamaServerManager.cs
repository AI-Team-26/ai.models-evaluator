using System.Diagnostics;

namespace Evaluator.Core;

internal sealed class FakeLlamaServerManager : IDisposable
{
    private Process? _process;
    
    public bool IsRunning => _process != null && !_process.HasExited;
    public int Port { get; private set; } = 8080;
    
    public async Task StartAsync(string modelPath, CancellationToken ct = default)
    {
        await Task.Yield();
        
        var port = FindAvailablePort(8080);
        Port = port;
        
        Console.WriteLine($"[Fake] Would start llama-server for '{modelPath}' on port {port}");
    }
    
    public void Stop(bool force = false)
    {
        if (_process == null || _process.HasExited) return;
        
        try
        {
            if (!force)
                _process.Kill();
            
            _process.Dispose();
        }
        catch
        { 
        }
        
        _process = null;
    }
    
    public void Dispose() => Stop(false);
    
    private static int FindAvailablePort(int preferredPort)
    {
        // TODO: Implement actual port availability check
        return preferredPort;
    }
}
