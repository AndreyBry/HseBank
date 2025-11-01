using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.Domain.Models.DTOs;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    public class OperationMenu : MenuExtensions, IOperationMenu
    {
        private IBankAccountFacade _bankAccountFacade;
        private ICategoryFacade _categoryFacade;
        private IOperationFacade _operationFacade;

        public OperationMenu(IBankAccountFacade bankAccountFacade, ICategoryFacade categoryFacade, IOperationFacade operationFacade)
        {
            _bankAccountFacade = bankAccountFacade;
            _categoryFacade = categoryFacade;
            _operationFacade = operationFacade;
        }

        public void Show()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Операции:[/]")
                    .AddChoices(new[] {
                        "📈 Добавить доход",
                        "📉 Добавить расход",
                        "🔙 Назад"
                    }));

            switch (choice)
            {
                case "📈 Добавить доход":
                    AddOperation(TransactionType.Income);
                    break;
                case "📉 Добавить расход":
                    AddOperation(TransactionType.Expense);
                    break;
            }
        }

        public void AddOperation(TransactionType type)
        {
            var operationType = type == TransactionType.Income ? "📈 ДОХОД" : "📉 РАСХОД";
            AnsiConsole.MarkupLine($"[yellow]{operationType}[/]");

            var accountsResult = _bankAccountFacade.GetAll();
            if (!accountsResult.IsSuccess || !accountsResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет счетов для операции[/]");
                WaitForKey();
                return;
            }

            var accountNames = accountsResult.Data
                .Select(a => $"{a.Name} ([grey]{a.Balance:C}[/])")
                .ToList();

            var accountChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите счет:[/]")
                    .AddChoices(accountNames));

            var selectedAccount = accountsResult.Data.ToList()[accountNames.IndexOf(accountChoice)];

            var categoriesResult = _categoryFacade.GetByType(type);
            if (!categoriesResult.IsSuccess || !categoriesResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет категорий для этого типа операции[/]");
                WaitForKey();
                return;
            }

            var categoryNames = categoriesResult.Data
                .Select(c => c.Name)
                .ToList();

            var categoryChoice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите категорию:[/]")
                    .AddChoices(categoryNames));

            var selectedCategory = categoriesResult.Data.ToList()[categoryNames.IndexOf(categoryChoice)];

            var amount = AnsiConsole.Ask("Сумма:", 100.00m);
            var description = AnsiConsole.Ask("Описание (необязательно):", "");

            var request = new OperationApplyRequest(type, selectedAccount.Id, amount, selectedCategory.Id, description);

            var result = _operationFacade.Apply(request);

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Операция выполнена успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }
    }
}
