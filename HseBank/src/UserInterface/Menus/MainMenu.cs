using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Основной меню.
    /// </summary>
    public class MainMenu : IMenu
    {
        private IBankAccountMenu _bankAccountMenu;
        private ICategoryMenu _categoryMenu;
        private IOperationMenu _operationMenu;
        private IMetricsMenu _metricsMenu;
        private IImportMenu _importMenu;
        private IExportMenu _exportMenu;
        private IUndoRedoMenu _undoRedoMenu;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bankAccountMenu">Меню для работы с банковскими счетами.</param>
        /// <param name="categoryMenu">Меню для работы с категориями.</param>
        /// <param name="operationMenu">Меню для работы с операциями.</param>
        /// <param name="metricsMenu">Меню для работы с метриками.</param>
        /// <param name="importMenu">Меню для импорта данных.</param>
        /// <param name="exportMenu">Меню для экспорта данных.</param>
        /// <param name="undoRedoMenu">Меню для работы с историей команд.</param>
        public MainMenu(IBankAccountMenu bankAccountMenu, ICategoryMenu categoryMenu, IOperationMenu operationMenu, 
            IMetricsMenu metricsMenu, IImportMenu importMenu, IExportMenu exportMenu, IUndoRedoMenu undoRedoMenu)
        {
            _bankAccountMenu = bankAccountMenu;
            _categoryMenu = categoryMenu;
            _operationMenu = operationMenu;
            _metricsMenu = metricsMenu;
            _importMenu = importMenu;
            _exportMenu = exportMenu;
            _undoRedoMenu = undoRedoMenu;
        }

        /// <summary>
        /// Вывод меню и выбор действия или подменю.
        /// </summary>
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
                            "📥 Импорт данных",
                            "📤 Экспорт данных",
                            "📊 Метрики производительности",
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
                    case "📊 Метрики производительности":
                        _metricsMenu.Show();
                        break;
                    case "📥 Импорт данных":
                        _importMenu.Show();
                        break;
                    case "📤 Экспорт данных":
                        _exportMenu.Show();
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
