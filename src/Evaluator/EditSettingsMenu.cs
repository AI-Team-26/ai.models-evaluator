using Spectre.Console;

namespace Evaluator;

public sealed class SettingsView
{
    public static void Run()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("\n[dim]=========================================[/]");
            AnsiConsole.MarkupLine("[dim]         Settings Editor[/]");
            AnsiConsole.MarkupLine("[dim]=========================================[/]");

            ShowCurrentSettings();

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("\n1. Edit 2. Add model 3. Edit model 4. Remove model 5. Exit")
                    .AddChoices("Edit", "Add model", "Edit model", "Remove model", "Exit"));

            switch (choice)
            {
                case "Edit":
                    EditGeneralSettings();
                    break;
                case "Add model":
                    AddModel();
                    break;
                case "Edit model":
                    EditModel();
                    break;
                case "Remove model":
                    RemoveModel();
                    break;
                case "Exit":
                    SaveAndExit();
                    return;
            }
        }
    }

    private static void ShowCurrentSettings()
    {
        var s = SettingsManager.Instance.LoadCurrent();
        if (s == null) return;

        var serverStatus = string.IsNullOrEmpty(s.LlamaCppPath) ? "[red](empty)[/]" : s.LlamaCppPath;
        AnsiConsole.MarkupLine($"\n[cyan]LLama Server:[/] {serverStatus}");
        AnsiConsole.MarkupLine($"[cyan]Default Port:[/] {s.DefaultPort}");

        if (s.Models.Count > 0)
        {
            AnsiConsole.MarkupLine("\n[green]Models:[/]");
            for (int i = 0; i < s.Models.Count; i++)
            {
                var m = s.Models[i];
                AnsiConsole.MarkupLine($"  [cyan]#{i + 1}[/] {m.Id}: {m.GgufFileName}");
            }
        }
        else
        {
            AnsiConsole.MarkupLine("\n[red]No models configured.[/]");
        }
    }

    private static void EditGeneralSettings()
    {
        var current = SettingsManager.Instance.LoadCurrent();
        if (current == null)
        {
            AnsiConsole.MarkupLine("[red]Unable to load settings.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"\n[green]Current server path:[/] {current.LlamaCppPath}");
        AnsiConsole.MarkupLine($"[green]Current default port:[/] {current.DefaultPort}");

        AnsiConsole.MarkupLine("\n[bold]Enter new server path (empty to keep current):[/]");
        var newPath = Console.ReadLine();

        if (!string.IsNullOrEmpty(newPath))
        {
            if (File.Exists(newPath))
            {
                current.LlamaCppPath = newPath;
                SettingsManager.Instance.Save(current);
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
                var p = int.Parse(portStr);
                if (p > 0 && p < 65536)
                {
                    current.DefaultPort = p;
                    SettingsManager.Instance.Save(current);
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
    }

    private static void AddModel()
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

    private static void EditModel()
    {
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
    }

    private static void RemoveModel()
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
                .Title("[bold]Select model to remove:[/]")
                .AddChoices(ids));

        settings.Models.RemoveAll(m => m.Id == selectedId);
        SettingsManager.Instance.Save(settings);
        AnsiConsole.MarkupLine($"[green]✓ Removed model '[cyan]{selectedId}[/]'[/].");
    }

    private static void SaveAndExit()
    {
        AnsiConsole.MarkupLine("\n[yellow]Configuration saved. Restart the app to apply changes.[/]");
    }
}
