using System.Diagnostics;

namespace Evaluator;

internal sealed class LlamaServerManager : IDisposable
{
    public bool IsRunning { get; private set; }
    public int Port { get; private set; }
    
    public void StartServer(string modelPath, int port)
    {
        // TODO: Implement real Process.Start for llama-server
        Port = port;
        throw new NotImplementedException();
    }
    
    public void StopServer(bool force = false)
    {
        // TODO: Implement real process termination
        throw new NotImplementedException();
    }
    
    public string ServerInfo()
    {
        // TODO: Return server status info
        throw new NotImplementedException();
    }
    
    public string CallApi(string endpoint, string payload)
    {
        // TODO: Call llama-server HTTP API
        throw new NotImplementedException();
    }
    
    public void Dispose()
    {
        if (IsRunning)
            StopServer();
    }
}
