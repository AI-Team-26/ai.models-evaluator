using Spectre.Console;

namespace Evaluator;

/// <summary>
/// Interactive menu system for editing application settings via CLI prompts.
/// Replaces manual JSON editing with guided input validation.
/// </summary>
public sealed class EditSettingsMenu
{
    /// <summary>
    /// Launches the interactive settings editor.
    /// Shows main menu with options to edit fields or add models.
    /// </summary>
    public static void Run()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("\n[dim]=========================================[/]");
            AnsiConsole.MarkupLine("[dim]         Configuration Editor[/]");
            AnsiConsole.MarkupLine("[dim]=========================================[/]");

            // Show current state summary
            AnsiConsole.MarkupLine($"[cyan]LLama Server:[/] {_currentServerStatus()}");
            AnsiConsole.MarkupLine($"[cyan]Models Loaded:[/] {_countModels()}");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\nSelect action:")
                    .AddChoices("Edit Server Path", "Edit Default Port", 
                                "Manage Models", "Save & Exit"));

            switch (choice)
            {
                case "Edit Server Path":
                    _editServerPath();
                    break;
                case "Edit Default Port":
                    _editDefaultPort();
                    break;
                case "Manage Models":
                    _manageModels();
                    break;
                case "Save & Exit":
                    _saveAndExit();
                    return;
            }
        }
    }

    private static string _currentServerStatus()
    {
        var s = SettingsManager.Instance.LoadCurrent();
        return string.IsNullOrEmpty(s?.LlamaCppPath) ? "[red]Not configured[/]" : s!.LlamaCppPath;
    }

    private static int _countModels()
    {
        var s = SettingsManager.Instance.LoadCurrent();
        return s?.Models.Count ?? 0;
    }

    private static void _editServerPath()
    {
        var current = SettingsManager.Instance.LoadCurrent();
        if (current == null)
        {
            AnsiConsole.MarkupLine("[red]Unable to load settings.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\nCurrent server path:\n[cyan]{current.LlamaCppPath}[/]\n");

        while (true)
        {
            AnsiConsole.MarkupLine("Enter full path to llama-server executable:");
            var newPath = Console.ReadLine();

            if (!string.IsNullOrEmpty(newPath) && File.Exists(newPath))
            {
                current.LlamaCppPath = newPath;
                SettingsManager.Instance.Save(current);
                AnsiConsole.MarkupLine("[green]✓ Server path updated.[/]\n");
                break;
            }
            else if (!string.IsNullOrEmpty(newPath))
            {
                AnsiConsole.MarkupLine("[red]✗ Path does not exist. Try again.[/]\n");
            }
        }
    }

    private static void _editDefaultPort()
    {
        var current = SettingsManager.Instance.LoadCurrent();
        if (current == null)
        {
            AnsiConsole.MarkupLine("[red]Unable to load settings.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\nCurrent default port: [cyan]{current.DefaultPort}[/]\n");

        while (true)
        {
            AnsiConsole.MarkupLine("Enter default port (empty keeps unchanged):");
            var portStr = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(portStr))
                return; // keep unchanged

            try
            {
                var p = int.Parse(portStr);
                if (p > 0 && p < 65536)
                {
                    current.DefaultPort = p;
                    SettingsManager.Instance.Save(current);
                    AnsiConsole.MarkupLine("[green]✓ Default port updated.[/]\n");
                    return;
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]✗ Must be number between 1-65535.[/]\n");
                }
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]✗ Invalid number format.[/]\n");
            }
        }
    }

    private static void _manageModels()
    {
        while (true)
        {
            AnsiConsole.MarkupLine($"\n[dim]=========================================[/]");
            AnsiConsole.MarkupLine("[dim]           Model Management[/]");
            AnsiConsole.MarkupLine("[dim]=========================================[/]");

            var choices = new List<string> { "Add New Model", "List All Models" };
            if (_countModels() > 0)
                choices.Add("Remove Model");

            choices.Add("Back");

            var choice = AnsiConsole.Prompt(new SelectionPrompt<string>().Title("").AddChoices(choices));

            switch (choice)
            {
                case "Add New Model":
                    _addModel();
                    break;
                case "List All Models":
                    _listModels();
                    break;
                case "Remove Model":
                    _removeModel();
                    break;
                case "Back":
                    return;
            }
        }
    }

    private static void _addModel()
    {
        AnsiConsole.MarkupLine("\n[dim]Adding new model configuration...[/]\n");

        AnsiConsole.MarkupLine("Model ID (required):");
        var id = Console.ReadLine();
        if (string.IsNullOrEmpty(id)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        AnsiConsole.MarkupLine("GGUF filename (required):");
        var gguf = Console.ReadLine();
        if (string.IsNullOrEmpty(gguf)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        AnsiConsole.MarkupLine("Context size (default: 2048, empty for default):");
        var ctxSizeStr = Console.ReadLine();
        var ctxSize = string.IsNullOrWhiteSpace(ctxSizeStr) ? 2048 : int.Parse(ctxSizeStr);

        AnsiConsole.MarkupLine("GPU layers (default: 1, empty for default):");
        var gpuLayersStr = Console.ReadLine();
        var gpuLayers = string.IsNullOrWhiteSpace(gpuLayersStr) ? 1 : int.Parse(gpuLayersStr);

        AnsiConsole.MarkupLine("CPU MoE threads (empty for 0):");
        var cpuMoEInput = Console.ReadLine();
        var cpuMoE = string.IsNullOrWhiteSpace(cpuMoEInput) ? 0 : int.Parse(cpuMoEInput);

        AnsiConsole.MarkupLine("Enable Jinja? (y/n, empty=n):");
        var jinjaInput = Console.ReadLine();
        bool jinja = false;
        if (!string.IsNullOrEmpty(jinjaInput))
        {
            jinja = jinjaInput.ToLower().StartsWith('y');
        }

        var manager = SettingsManager.Instance;
        var settings = manager.LoadCurrent();
        if (settings == null) return;

        settings.Models ??= [];
        settings.Models.Add(new ModelSettings
        {
            Id = id,
            GgufFileName = gguf,
            ContextSize = ctxSize,
            GpuLayers = gpuLayers,
            CpuMoE = cpuMoE,
            Jinja = jinja
        });

        manager.Save(settings);
        AnsiConsole.MarkupLine($"[green]✓ Model '{id}' added.[/]");
    }

    private static void _listModels()
    {
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[yellow]No models configured.[/]");
            return;
        }

        AnsiConsole.MarkupLine("\n[dim]Configured models:[/]");
        foreach (var m in settings!.Models)
        {
            AnsiConsole.MarkupLine($"  [cyan]{m.Id}[/]: {m.GgufFileName}");
        }
    }

    private static void _removeModel()
    {
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models to remove.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToList();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Select model to remove:")
                .AddChoices(ids));

        settings.Models.RemoveAll(m => m.Id == selectedId);
        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine($"[green]✓ Removed model '[cyan]{selectedId}[/]'[/].");
    }

    private static void _saveAndExit()
    {
        // Save is already done on each edit
        AnsiConsole.MarkupLine("\n[yellow]Configuration saved. Restart the app to apply changes.[/]");
    }
}
