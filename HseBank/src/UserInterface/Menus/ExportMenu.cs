using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Меню для экспорта данных в файл.
    /// </summary>
    public class ExportMenu : MenuExtensions, IExportMenu
    {
        private IExportFacade _exportFacade;
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="exportFacade">Фасад экспорта.</param>
        public ExportMenu(IExportFacade exportFacade)
        {
            _exportFacade = exportFacade;
        }

        /// <summary>
        /// Вывод меню и выбор действия.
        /// </summary>
        public void Show()
        {
            var choice = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("Выберите формат экспорта:")
                .AddChoices("📊 JSON", "📋 CSV", "🔙 Назад"));

            switch (choice)
            {
                case "📊 JSON":
                    ExportToJson();
                    break;
                case "📋 CSV":
                    ExportToCsv();
                    break;
            }
        }

        /// <summary>
        /// Запуск процесса экспорта данных в JSON-файл.
        /// </summary>
        public void ExportToJson()
        {
            var filePath = AnsiConsole.Ask<string>("Путь для сохранения JSON (относительно папки data):", "export/data.json");
            filePath = Path.Combine("../../../../data/", filePath);

            var result = _exportFacade.Export(filePath, FileFormat.Json);

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Данные экспортированы успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        /// <summary>
        /// Запуск процесса экспорта данных в CSV-файл.
        /// </summary>
        public void ExportToCsv()
        {
            var filePath = AnsiConsole.Ask<string>("Путь для сохранения CSV (относительно папки data):", "export/data.csv");
            filePath = Path.Combine("../../../../data/", filePath);

            var result = _exportFacade.Export(filePath, FileFormat.Csv);

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Данные экспортированы успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }
    }
}
