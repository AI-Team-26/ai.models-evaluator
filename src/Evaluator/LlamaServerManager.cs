using System.Diagnostics;

namespace Evaluator;

internal sealed class LlamaServerManager
{
    public bool IsRunning { get; private set; }
    public int Port { get; private set; }
    public string ServerUrl { get; private set; } = "";

    public void StartServer(string modelPath, int port)
    {
        // TODO: Implement real Process.Start for llama-server
        Port = port;
        ServerUrl = $"http://localhost:{port}";
        throw new NotImplementedException();
    }

    public void StopServer(bool force = false)
    {
        // TODO: Implement real process termination
        throw new NotImplementedException();
    }



    public string CallApi(string endpoint, string payload)
    {
        // TODO: Call llama-server HTTP API
        throw new NotImplementedException();
    }
}
