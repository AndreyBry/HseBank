using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.Domain.Models.DTOs;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    /// <summary>
    /// Меню для работы с категориями.
    /// </summary>
    public class CategoryMenu : MenuExtensions, ICategoryMenu
    {
        private ICategoryFacade _categoryFacade;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="categoryFacade">Фасад категорий.</param>
        public CategoryMenu(ICategoryFacade categoryFacade)
        {
            _categoryFacade = categoryFacade;
        }

        /// <summary>
        /// Вывод меню и выбор действия.
        /// </summary>
        public void Show()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Управление категориями:[/]")
                    .AddChoices(new[] {
                        "➕ Создать категорию",
                        "📝 Изменить название категории",
                        "🗑️ Удалить категорию",
                        "📋 Список категорий",
                        "🔙 Назад"
                    }));

            switch (choice)
            {
                case "➕ Создать категорию":
                    CreateCategory();
                    break;
                case "📝 Изменить название категории":
                    ChangeCategoryName();
                    break;
                case "🗑️ Удалить категорию":
                    DeleteCategory();
                    break;
                case "📋 Список категорий":
                    ShowCategories();
                    break;
            }
        }

        /// <summary>
        /// Запуск процесса создания категории.
        /// </summary>
        public void CreateCategory()
        {
            AnsiConsole.MarkupLine("[yellow]➕ СОЗДАНИЕ КАТЕГОРИИ[/]");

            var typeChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Тип категории:[/]")
                    .AddChoices("📈 Доход", "📉 Расход"));

            var type = typeChoice == "📈 Доход" ? TransactionType.Income : TransactionType.Expense;
            var name = AnsiConsole.Ask<string>("[white]Название категории:[/]");

            var result = _categoryFacade.Create(new CategoryCreateRequest(type, name.Trim()));

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Категория создана успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        /// <summary>
        /// Запуск процесса изменения названия категории.
        /// </summary>
        public void ChangeCategoryName()
        {
            var categoriesResult = _categoryFacade.GetAll();
            if (!categoriesResult.IsSuccess || !categoriesResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет категорий для изменения названия[/]");
                WaitForKey();
                return;
            }

            var categoryNames = categoriesResult.Data
                .Select(c => c.Name)
                .ToList();
            categoryNames.Add("🔙 Назад");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите категорию для удаления:[/]")
                    .AddChoices(categoryNames));

            if (choice == "🔙 Назад") return;

            var name = AnsiConsole.Ask<string>("[white]Название категории:[/]");

            var result = _categoryFacade.ChangeName(choice, name.Trim());
            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Название категории изменено![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        /// <summary>
        /// Запуск процесса удаления категории.
        /// </summary>
        public void DeleteCategory()
        {
            var categoriesResult = _categoryFacade.GetAll();
            if (!categoriesResult.IsSuccess || !categoriesResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет категорий для удаления[/]");
                WaitForKey();
                return;
            }

            var categoryNames = categoriesResult.Data
                .Select(c => $"{c.Name} ([grey]{c.Type}[/])")
                .ToList();
            categoryNames.Add("🔙 Назад");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите категорию для удаления:[/]")
                    .AddChoices(categoryNames));

            if (choice == "🔙 Назад") return;

            var selectedCategory = categoriesResult.Data.ToList()[
                categoryNames.IndexOf(choice)
            ];

            if (AnsiConsole.Confirm($"[red]Удалить категорию '{selectedCategory.Name}'?[/]", false))
            {
                var result = _categoryFacade.Delete(selectedCategory.Id);
                if (result.IsSuccess)
                {
                    AnsiConsole.MarkupLine("[green]✓ Категория удалена![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
                }
            }

            WaitForKey();
        }

        /// <summary>
        /// Отображение всех категорий.
        /// </summary>
        public void ShowCategories()
        {
            var result = _categoryFacade.GetAll();
            if (!result.IsSuccess || !result.Data.Any())
            {
                AnsiConsole.MarkupLine("[yellow]Нет созданных категорий[/]");
                WaitForKey();
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Title("[yellow]📋 КАТЕГОРИИ[/]");

            table.AddColumn("Название");
            table.AddColumn("Тип");

            foreach (var category in result.Data)
            {
                var typeColor = category.Type == TransactionType.Income ? "green" : "red";
                var typeText = category.Type == TransactionType.Income ? "📈 Доход" : "📉 Расход";

                table.AddRow(
                    category.Name,
                    $"[{typeColor}]{typeText}[/]"
                );
            }

            AnsiConsole.Write(table);
            WaitForKey();
        }
    }
}
