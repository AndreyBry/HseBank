using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Абстрактный класс, описывающий утилиты для меню.
    /// </summary>
    public abstract class MenuExtensions
    {
        /// <summary>
        /// Ожидание нажатия клавишы пользователем.
        /// </summary>
        protected void WaitForKey()
        {
            AnsiConsole.MarkupLine("\n[grey]Нажмите любую клавишу для продолжения...[/]");
            Console.ReadKey();
        }
    }
}
