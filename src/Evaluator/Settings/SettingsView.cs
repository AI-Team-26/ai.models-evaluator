using Evaluator.Settings;
using Evaluator.UI;
using Spectre.Console;
using System.Data;

namespace Evaluator;

public sealed class SettingsView() : View("Settings")
{
    public void Run()
    {
        Clear();

        if (SettingsManager.HasSettings)
        {
            ShowCurrentSettings();
            ShowMenu();
        }
        else
        {
            AnsiConsole.MarkupLine("Settings are not set. Set it now.");
            EditGeneralSettings();
        }
    }


    enum M
    {
        EditSettings,
        AddModel
    }

    record struct Menu
    {
        public const string EditSettings = "Edit settings";
        public const string AddModel = "Add model";
    }

    private void ShowMenu()
    {
        while (true)
        {
            Clear();
            AnsiConsole.MarkupLine($"[gray]Settimgs are stored in \"{SettingsManager.SettingsFilePath}\".[/]");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    //.Title("\nSelect an option:")
                    .AddChoices(Menu.EditSettings, Menu.AddModel, "Edit model", "Remove model", "Exit"));

            switch (choice)
            {
                case Menu.EditSettings:
                    EditGeneralSettings();
                    break;
                case Menu.AddModel:
                    AddModel();
                    break;
                case "Edit model":
                    EditModel();
                    break;
                case "Remove model":
                    RemoveModel();
                    break;
                case "Exit":
                    //SaveAndExit();
                    return;
            }
        }
    }

    private static void ShowCurrentSettings()
    {
        ApplicationSettings settings = SettingsManager.GetSettings();
        var serverStatus = string.IsNullOrEmpty(settings.LlamaCppPath) ? "[red](empty)[/]" : settings.LlamaCppPath;
        AnsiConsole.MarkupLine($"[cyan]LLama Server:[/] {serverStatus}");
        AnsiConsole.MarkupLine($"[cyan]Default Port:[/] {settings.DefaultPort}");

        if (settings.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[green]Models:[/]");
            for (int i = 0; i < settings.Models.Count; i++)
            {
                var m = settings.Models[i];
                AnsiConsole.MarkupLine($"  [cyan]#{i + 1}[/] {m.Id}: {m.GgufFileName}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[red]No models configured.[/]");
        }
    }

    private void EditGeneralSettings()
    {
        ShowCurrentSettings();

        ApplicationSettings newSettings = 
            SettingsManager.HasSettings ? 
                SettingsManager.GetSettings() with { } : // create a copy
                new ApplicationSettings(); // new empty

        AnsiConsole.MarkupLine($"\n[green]Current server path:[/] {newSettings.LlamaCppPath}");
        AnsiConsole.MarkupLine($"[green]Current default port:[/] {newSettings.DefaultPort}");

        AnsiConsole.MarkupLine("\n[bold]Enter new server path (empty to keep current):[/]");
        var newPath = Console.ReadLine();

        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath))
            {
                newSettings.LlamaCppPath = newPath;
                AnsiConsole.MarkupLine("[green]✓ Server path updated.[/]\n");
            }
            else
            {
                AnsiConsole.MarkupLine("[red]✗ Path does not exist. Keeping current value.[/]\n");
            }
        }

        AnsiConsole.MarkupLine("[bold]Enter new default port (empty to keep current):[/]");
        var portStr = Console.ReadLine();

        if (!string.IsNullOrWhiteSpace(portStr))
        {
            try
            {
                var port = int.Parse(portStr);
                if (port > 0 && port < 65536)
                {
                    newSettings.DefaultPort = port;
                    AnsiConsole.MarkupLine("[green]✓ Default port updated.[/]\n");
                }
                else
                {
                    AnsiConsole.MarkupLine("[red]✗ Must be number between 1-65535. Keeping current value.[/]\n");
                }
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]✗ Invalid number format. Keeping current value.[/]\n");
            }
        }

        try
        {
            SettingsManager.Save(newSettings);
            Success("Settings saved");
        }
        catch (Exception exc)
        {
            Error("Failed to save Settings", exc);
        }
    }

    private void AddModel()
    {
        AnsiConsole.MarkupLine("\n[dim]Adding new model configuration...[/]\n");

        AnsiConsole.MarkupLine("[bold]Model ID (required):[/]");
        var id = Console.ReadLine();
        if (string.IsNullOrEmpty(id)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        AnsiConsole.MarkupLine("[bold]GGUF filename (required):[/]");
        var gguf = Console.ReadLine();
        if (string.IsNullOrEmpty(gguf)) { AnsiConsole.MarkupLine("[red]Cancelled.[/]"); return; }

        AnsiConsole.MarkupLine("[bold]Context size (default: 2048, empty for default):[/]");
        var ctxSizeStr = Console.ReadLine();
        var ctxSize = string.IsNullOrWhiteSpace(ctxSizeStr) ? 2048 : int.Parse(ctxSizeStr);

        AnsiConsole.MarkupLine("[bold]GPU layers (default: 1, empty for default):[/]");
        var gpuLayersStr = Console.ReadLine();
        var gpuLayers = string.IsNullOrWhiteSpace(gpuLayersStr) ? 1 : int.Parse(gpuLayersStr);

        AnsiConsole.MarkupLine("[bold]CPU MoE threads (empty for 0):[/]");
        var cpuMoEInput = Console.ReadLine();
        var cpuMoE = string.IsNullOrWhiteSpace(cpuMoEInput) ? 0 : int.Parse(cpuMoEInput);

        AnsiConsole.MarkupLine("[bold]Enable Jinja? (y/n, empty=n):[/]");
        var jinjaInput = Console.ReadLine();
        bool jinja = false;
        if (!string.IsNullOrEmpty(jinjaInput))
        {
            jinja = jinjaInput.ToLower().StartsWith('y');
        }

        var settings = SettingsManager.GetSettings();

        // TODO: see how to update the settings wiuth the new model
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

        SettingsManager.Save(settings);
        Success($"Model '{id}' added.");
    }

    private void EditModel()
    {
        throw new Exception("not implemented");

        /*
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models to edit.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToList();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Select model to edit:[/]")
                .AddChoices(ids));

        var model = settings.Models.FirstOrDefault(m => m.Id == selectedId);
        if (model == null) return;

        AnsiConsole.MarkupLine($"\n[green]Editing model: {model.Id}[/]");
        AnsiConsole.MarkupLine($"[green]Current GGUF filename:[/] {model.GgufFileName}");
        AnsiConsole.MarkupLine($"[green]Current context size:[/] {model.ContextSize}");
        AnsiConsole.MarkupLine($"[green]Current GPU layers:[/] {model.GpuLayers}");

        AnsiConsole.MarkupLine("\n[bold]Enter new GGUF filename (empty to keep current):[/]");
        var gguf = Console.ReadLine();
        if (!string.IsNullOrEmpty(gguf))
        {
            model.GgufFileName = gguf;
        }

        AnsiConsole.MarkupLine("[bold]Enter new context size (empty to keep current):[/]");
        var ctxSizeStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(ctxSizeStr))
        {
            try
            {
                model.ContextSize = int.Parse(ctxSizeStr);
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]");
            }
        }

        AnsiConsole.MarkupLine("[bold]Enter new GPU layers (empty to keep current):[/]");
        var gpuLayersStr = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(gpuLayersStr))
        {
            try
            {
                model.GpuLayers = int.Parse(gpuLayersStr);
            }
            catch
            {
                AnsiConsole.MarkupLine("[red]Invalid number. Keeping current value.[/]");
            }
        }

        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine("[green]✓ Model updated.[/]");
        */
    }

    private void RemoveModel()
    {
        throw new Exception("not implemented");
        /*
        var settings = SettingsManager.Instance.LoadCurrent();
        if (settings == null || settings.Models.Count == 0)
        {
            AnsiConsole.MarkupLine("[red]No models to remove.[/]");
            return;
        }

        var ids = settings.Models.Select(m => m.Id).ToList();
        var selectedId = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("[bold]Select model to remove:[/]")
                .AddChoices(ids));

        settings.Models.RemoveAll(m => m.Id == selectedId);
        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine($"[green]✓ Removed model '[cyan]{selectedId}[/]'[/].");
        */
    }
}
