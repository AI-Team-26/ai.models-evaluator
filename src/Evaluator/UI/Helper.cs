using Spectre.Console;

namespace Evaluator.UI
{
    internal static class Helper
    {
        public static string GetInput(string label)
        {
            AnsiConsole.MarkupLine($"[gray]{label}: [/]");
            return Console.ReadLine() ?? "";
        }

        public static void Clear()
        {
            Console.Clear();
            AnsiConsole.Write(new FigletText("LLM Evaluator"));
        }
    }
}
