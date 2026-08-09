using Evaluator.Settings;
using Spectre.Console;

namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            // Force to set Settings if not exists (first run of the app)
            if (!SettingsManager.HasSettings)
                ViewSettings();

            var serverManager = new LlamaServerManager();
            var evaluator = new Evaluator(serverManager);

            const string SeeResults    = "See Results";
            const string RunEval       = "Run Evaluation";
            const string ViewSettings_ = "View Settings";
            const string Exit          = "Exit";

            while (true)
            {
                UI.UI.Clear();
                
                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(RunEval, SeeResults, ViewSettings_, Exit));

                switch (choice)
                {
                    case RunEval:
                        await RunEvaluationAsync(evaluator);
                        break;
                    case SeeResults:
                        await ShowResultsAsync();
                        break;
                    case ViewSettings_:
                        ViewSettings();
                        break;
                    case Exit:
                        return 0;
                }
            }
        }
        catch (Exception configEx)
        {
            AnsiConsole.MarkupLine("\n[dim]=========================================[/]");
            AnsiConsole.MarkupLine("[yellow]⚠️  Settings Incomplete![/]");
            AnsiConsole.MarkupLine("[dim]=========================================[/]");
            AnsiConsole.MarkupLine($"[red]Error:[/] {configEx.Message}");
            AnsiConsole.MarkupLine("\nStarting Settings Editor...");

            ViewSettings();

            AnsiConsole.MarkupLine("\n[yellow]Press any key to exit...[/]");
            Console.ReadKey(true);
            return 1;
        }
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

    private static void ViewSettings()
    {
        new SettingsView().Run();
    }
}
