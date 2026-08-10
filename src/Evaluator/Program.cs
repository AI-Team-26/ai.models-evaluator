using Evaluator.Settings;
using Evaluator.UI;
using Spectre.Console;

namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        try
        {
            if (!SettingsManager.HasSettings)
            {
                AnsiConsole.MarkupLine("[yellow]⚠ Settings are not configured. Some features like Run Evaluation will not work until you set them up.[/]");
                AnsiConsole.MarkupLine("[dim]You can configure settings later via the View Settings menu option.[/]");
                AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                Console.ReadKey(true);
            }

            var serverManager = new LlamaServerManager();
            var evaluator = new Evaluator(serverManager);

            const string SeeResults = "See Results";
            const string RunEval = "Run Evaluation";
            const string ViewSettings_ = "View Settings";
            const string Exit = "❌ Exit";

            while (true)
            {
                Helper.Clear();

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .AddChoices(RunEval, SeeResults, ViewSettings_, Exit));

                switch (choice)
                {
                    case RunEval:
                        if (!SettingsManager.HasSettings)
                        {
                            AnsiConsole.MarkupLine("[red]⚠ Settings are not configured. Please set up settings first via the View Settings menu.[/]");
                            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
                            Console.ReadKey(true);
                            break;
                        }
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
        catch (Exception exc)
        {
            AnsiConsole.WriteException(exc);
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