using Spectre.Console;

namespace Evaluator.UI
{
    internal static class UI
    {
        public static void Clear() {
            Console.Clear();
            AnsiConsole.Write(new FigletText("LLM Evaluator"));
        }
    }
}
