using Spectre.Console;

namespace Evaluator;

public static class Program
{
    public static async Task<int> Main(string[] args)
    {
        var serverManager = new LlamaServerManager();
        var evaluator = new Evaluator(serverManager);

        while (true)
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

    private static void ChangeSettings()
    {
        AnsiConsole.MarkupLine("[dim]Settings editor not yet implemented.[/] ");
    }
}
