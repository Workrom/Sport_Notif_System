using Spectre.Console;
namespace SRS
{
    internal class Program
    {
        private static Style DGreyStyle = new Style(foreground: Color.DarkSlateGray3, background: Color.Grey19);
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                var mainChoice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("Main Menu - Choose an [green]option[/]:")
                        .PageSize(5)
                        .HighlightStyle(DGreyStyle)
                        .AddChoices(new[] { "Manage", "Settings", "Exit" }));

                switch (mainChoice)
                {
                    case "Manage":

                        break;
                    case "Settings":

                        break;
                    case "Exit":
                        AnsiConsole.MarkupLine("[red]Exiting...[/]");
                        return;
                }
            }
        }
    }
}