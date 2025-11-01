using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    public abstract class MenuExtensions
    {
        protected void WaitForKey()
        {
            AnsiConsole.MarkupLine("\n[grey]Нажмите любую клавишу для продолжения...[/]");
            Console.ReadKey();
        }
    }
}
