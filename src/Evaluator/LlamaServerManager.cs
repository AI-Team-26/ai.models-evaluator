using Evaluator.Settings;

namespace Evaluator;

internal sealed class LlamaServerManager
{
    public bool IsRunning { get; private set; }
    public int Port { get; private set; }
    public string ServerUrl { get; private set; } = "";

    public void StartServer(string modelId, int port)
    {
        var settings = SettingsManager.GetSettings();
        var modelConfig = settings.Models.FirstOrDefault(m => m.Id == modelId);
        
        if (modelConfig == null)
            throw new ArgumentException($"Model '{modelId}' not found in configuration.");

        string ggufPath = Path.Combine(settings.ModelsFolderPath, modelConfig.GgufFileName);

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
