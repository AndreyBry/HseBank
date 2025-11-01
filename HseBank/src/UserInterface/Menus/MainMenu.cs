using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    public class MainMenu : IMenu
    {
        private IBankAccountMenu _bankAccountMenu;
        private ICategoryMenu _categoryMenu;
        private IOperationMenu _operationMenu;
        private IUndoRedoMenu _undoRedoMenu;

        public MainMenu(IBankAccountMenu bankAccountMenu, ICategoryMenu categoryMenu, IOperationMenu operationMenu, IUndoRedoMenu undoRedoMenu)
        {
            _bankAccountMenu = bankAccountMenu;
            _categoryMenu = categoryMenu;
            _operationMenu = operationMenu;
            _undoRedoMenu = undoRedoMenu;
        }

        public void Show()
        {
            while (true)
            {
                AnsiConsole.Clear();

                var panel = new Panel("[bold yellow]🏦 HSE BANK - УЧЕТ ФИНАНСОВ[/]")
                   .Border(BoxBorder.Rounded)
                   .Header("[green]Главное меню[/]")
                   .Padding(1, 1, 1, 1);

                AnsiConsole.Write(panel);

                var choice = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .Title("[white]Выберите действие:[/]")
                        .PageSize(10)
                        .AddChoices(new[] {
                            "💰 Управление счетами",
                            "📂 Управление категориями",
                            "💳 Операции (доход/расход)",
                            "↩️ Отменить последнее действие",
                            "🔁 Повторить отмененное действие",
                            "❌ Выход"
                        }));

                switch (choice)
                {
                    case "💰 Управление счетами":
                        _bankAccountMenu.Show();
                        break;
                    case "📂 Управление категориями":
                        _categoryMenu.Show();
                        break;
                    case "💳 Операции (доход/расход)":
                        _operationMenu.Show();
                        break;
                    case "↩️ Отменить последнее действие":
                        _undoRedoMenu.UndoLastAction();
                        break;
                    case "🔁 Повторить отмененное действие":
                        _undoRedoMenu.RedoAction();
                        break;
                    case "❌ Выход":
                        return;
                }
            }
        }
    }
}
