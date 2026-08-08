using System.Diagnostics;
using System.Linq;

namespace Evaluator;

internal sealed class LlamaServerManager
{
    private readonly Configuration _config;

    public LlamaServerManager(Configuration config)
    {
        _config = config ?? throw new ArgumentNullException(nameof(config));
    }

    public bool IsRunning { get; private set; }
    public int Port { get; private set; }
    public string ServerUrl { get; private set; } = "";

    public void StartServer(string modelId, int port)
    {
        var modelConfig = _config.Models.FirstOrDefault(m => m.Id == modelId);
        if (modelConfig == null)
            throw new ArgumentException($"Model '{modelId}' not found in configuration.");

        string ggufPath = Path.Combine(_config.ModelsFilePath, modelConfig.GgufFileName);

        // TODO: Use 'ggufPath' for llama-server process start
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
