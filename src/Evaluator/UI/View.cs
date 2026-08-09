using Spectre.Console;
namespace Evaluator.UI
{
    public interface IView
    {
        void Clear();

        void Success(string text, bool wait = true);
        void Error(string text, bool wait = true);
        void Error(string text, Exception exc, bool wait = true);

        void PressAnyKeyToContinue();
    }

    public class View(string title) : IView
    {
        public void Clear()
        {
            Console.Clear();
            AnsiConsole.Write(new FigletText("LLM Evaluator"));
            AnsiConsole.MarkupLine($"[yellow dim]============================================================================================[/]");
            AnsiConsole.MarkupLine($"[yellow bold] {title}[/]");
            AnsiConsole.MarkupLine($"[yellow dim]============================================================================================[/]");
        }

        public void Success(string text, bool wait=true)
        {
            AnsiConsole.MarkupLine($"[green]✓ {text}[/]\n");
            if (wait) PressAnyKeyToContinue();
        }

        public void Error(string text, bool wait = true)
        {
            AnsiConsole.MarkupLine($"[red]✗ {text}[/]\n");
            if (wait) PressAnyKeyToContinue();
        }

        public void Error(string text, Exception exc, bool wait = true)
        {
            AnsiConsole.MarkupLine($"[red]✗ {text}[/]\n");
            AnsiConsole.WriteException(exc);
            if (wait) PressAnyKeyToContinue();
        }

        public void PressAnyKeyToContinue()
        {
            AnsiConsole.MarkupLine("\n[yellow]Press any key to continue...[/]");
            Console.ReadKey(true);
        }

        public void PressAnyKeyToExit()
        {
            AnsiConsole.MarkupLine("\n[yellow]Press any key to exit...[/]");
            Console.ReadKey(true);
        }
    }
}
