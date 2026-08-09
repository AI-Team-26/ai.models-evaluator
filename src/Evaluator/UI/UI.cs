using Spectre.Console;

namespace Evaluator.UI
{
    internal static class UI
    {
        public static string GetInput(string label)
        {
            AnsiConsole.MarkupLine($"[darkgray]{label}: [/]");
            return Console.ReadLine() ?? "";
        }

        public static void Clear()
        {
            Console.Clear();
            AnsiConsole.Write(new FigletText("LLM Evaluator"));
        }
    }
}
