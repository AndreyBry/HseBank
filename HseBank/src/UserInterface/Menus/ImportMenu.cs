using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Меню для импорта данных из файла.
    /// </summary>
    public class ImportMenu : MenuExtensions, IImportMenu
    {
        private IImportFacade _importFacade;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="importFacade">Фасад импорта.</param>
        public ImportMenu(IImportFacade importFacade)
        {
            _importFacade = importFacade;
        }

        /// <summary>
        /// Вывод меню и выбор действия.
        /// </summary>
        public void Show()
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите формат импорта:")
                .AddChoices("📊 JSON", "📋 CSV", "🔙 Назад"));

            switch (choice)
            {
                case "📊 JSON":
                    ImportFromJson();
                    break;
                case "📋 CSV":
                    ImportFromCsv();
                    break;
            }
        }

        /// <summary>
        /// Запуск процесса импорт данных из JSON-файла.
        /// </summary>
        public void ImportFromJson()
        {
            var filePath = AnsiConsole.Ask<string>("Путь для чтения JSON (относительно папки data):", "import/data.json");
            filePath = Path.Combine("../../../../data/", filePath);

            var result = _importFacade.Import(filePath, FileFormat.Json);

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Данные импортированы успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        /// <summary>
        /// Запуск процесса импорта данных из CSV-файла.
        /// </summary>
        public void ImportFromCsv()
        {
            var filePath = AnsiConsole.Ask<string>("Путь для чтения CSV (относительно папки data):", "import/data.csv");
            filePath = Path.Combine("../../../../data/", filePath);

            var result = _importFacade.Import(filePath, FileFormat.Csv);

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Данные импортированы успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }
    }
}
