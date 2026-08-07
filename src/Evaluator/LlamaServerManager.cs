using System.Diagnostics;

namespace Evaluator;

internal sealed class LlamaServerManager : IDisposable
{
    private Process? _process;
    
    public bool IsRunning => _process != null && !_process.HasExited;
    public int Port { get; private set; }
    
    public async Task StartAsync(string modelPath, CancellationToken ct = default)
    {
        if (IsRunning)
            throw new InvalidOperationException("Server is already running");
        
        Port = FindAvailablePort(8080);
        
        var startInfo = new ProcessStartInfo
        {
            FileName = "llama-server",
            Arguments = $"-m \"{modelPath}\" --port {Port} --ctx-size 2048",
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };
        
        _process = new Process { StartInfo = startInfo };
        _process.OutputDataReceived += (_, e) => LogOutput(e.Data, "stdout");
        _process.ErrorDataReceived += (_, e) => LogOutput(e.Data, "stderr");
        
        _process.Start();
        _process.BeginOutputReadLine();
        _process.BeginErrorReadLine();
        
        await WaitForHealthCheck(ct);
    }
    
    public void Stop(bool force = false)
    {
        if (_process == null || _process.HasExited) return;
        
        if (force)
            _process.Kill(entireProcessTree: true);
        else
            _process.CloseMainWindow();
        
        _process.WaitForExit(5000);
        
        if (!_process.HasExited)
            _process.Kill(entireProcessTree: true);
        
        _process.Dispose();
        _process = null;
    }
    
    public void Dispose() => Stop(false);
    
    private static int FindAvailablePort(int preferredPort)
    {
        for (var port = preferredPort; port < preferredPort + 100; port++)
        {
            try
            {
                using var listener = new System.Net.Sockets.TcpListener(System.Net.IPAddress.Loopback, port);
                listener.Start();
                listener.Stop();
                return port;
            }
            catch
            {
                continue;
            }
        }
        
        throw new InvalidOperationException("No available port found");
    }
    
    private async Task WaitForHealthCheck(CancellationToken ct)
    {
        var maxWait = TimeSpan.FromSeconds(30);
        var startTime = DateTime.UtcNow;
        
        using var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(2) };
        
        while (DateTime.UtcNow - startTime < maxWait)
        {
            try
            {
                var response = await httpClient.GetAsync($"http://localhost:{Port}/health", ct);
                if (response.IsSuccessStatusCode)
                    return;
            }
            catch
            {
                await Task.Delay(500, ct);
            }
        }
        
        throw new TimeoutException($"Server did not become healthy within {maxWait.TotalSeconds}s");
    }
    
    private static void LogOutput(string? data, string stream)
    {
        if (string.IsNullOrWhiteSpace(data)) return;
        Console.WriteLine($"[{stream}] {data}");
    }
}
