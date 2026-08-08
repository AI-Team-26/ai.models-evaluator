using Spectre.Console;

namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var hasModels = IsConfigurationComplete();

        if (!hasModels)
        {
            AnsiConsole.MarkupLine("\n[yellow]WARNING: No models configured![]");
            AnsiConsole.MarkupLine("To use the AI Model Evaluator, you need to configure at least one model.");
            Console.WriteLine("Edit the settings file manually:");
            Console.WriteLine(SettingsManager.Instance.SettingsFilePath);
            Console.WriteLine();
            Console.WriteLine("Press Enter to view configuration instructions...");
            Console.ReadLine();
            
            ChangeSettings();
            Console.WriteLine();
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
            return 0;
        }

        var serverManager = new LlamaServerManager(SettingsManager.Instance);
        var evaluator = new Evaluator(serverManager);

        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[bold]AI Model Evaluator[/]")
                    .AddChoices("See Results", "Run Evaluation", "Change Settings", "Exit"));

            switch (choice)
            {
                case "See Results":
                    await ShowResultsAsync();
                    break;
                case "Run Evaluation":
                    await RunEvaluationAsync(evaluator);
                    break;
                case "Change Settings":
                    ChangeSettings();
                    break;
                case "Exit":
                    return 0;
            }
        }
        return 0;
    }

    private static async Task ShowResultsAsync()
    {
        var resultsDir = Path.Combine(AppContext.BaseDirectory, "..", "..", "results");
        if (!Directory.Exists(resultsDir))
        {
            AnsiConsole.MarkupLine("[red]No results directory found.[/] ");
            return;
        }

        var files = Directory.GetFiles(resultsDir, "evaluation_*.json");
        if (files.Length == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No results found.[/] ");
            return;
        }

        foreach (var file in files.OrderByDescending(f => f))
        {
            AnsiConsole.WriteLine(Path.GetFileName(file));
        }
    }

    private static async Task RunEvaluationAsync(Evaluator evaluator)
    {
        var modelId = AnsiConsole.Ask<string>("Enter model ID:");

        if (string.IsNullOrEmpty(modelId))
        {
            AnsiConsole.WriteLine("Model ID cannot be empty.");
            return;
        }

        try
        {
            await evaluator.EvaluateAsync(modelId);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupLine("[red]Error:[/] {0}", ex.Message);
        }
    }

    private static bool IsConfigurationComplete()
    {
        try
        {
            var settings = SettingsManager.Instance.Settings;
            return settings.Models.Count > 0;
        }
        catch
        {
            return false;
        }
    }

    private static void ChangeSettings()
    {
        var filePath = SettingsManager.Instance.SettingsFilePath;
        
        AnsiConsole.MarkupLine("\n[dim]=========================================[/");
        AnsiConsole.MarkupLine("[dim]   Manual Configuration Required[/]");
        AnsiConsole.MarkupLine("[dim]=========================================[/]");
        AnsiConsole.MarkupLine("\nPlease edit the following file and add your model configurations:\n");
        AnsiConsole.MarkupLine($"[cyan]{filePath}[/]");
        AnsiConsole.MarkupLine("\nExample JSON structure:\n");
        AnsiConsole.MarkupLine("[[yellow]]{");
        AnsiConsole.MarkupLine("  \"llamaCppPath\": \"path/to/llama-server\",");
        AnsiConsole.MarkupLine("  \"defaultPort\": 8001,");
        AnsiConsole.MarkupLine("  \"modelsFilePath\": \"/models\",");
        AnsiConsole.MarkupLine("  \"models\": [[");
        AnsiConsole.MarkupLine("    {");
        AnsiConsole.MarkupLine("      \"id\": \"model-name\",");
        AnsiConsole.MarkupLine("      \"ggufFileName\": \"model.gguf\",");
        AnsiConsole.MarkupLine("      \"contextSize\": 2048,");
        AnsiConsole.MarkupLine("      \"gpuLayers\": 99");
        AnsiConsole.MarkupLine("    }");
        AnsiConsole.MarkupLine("  ]");
        AnsiConsole.MarkupLine("}");
        AnsiConsole.MarkupLine("\n[dim]After editing, restart the application.[/]\n");
    }
}
