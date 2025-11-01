using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.Domain.Models.DTOs;
using Spectre.Console;

namespace HseBank.src.UserInterface.Menus
{
    public class BankAccountMenu : MenuExtensions, IBankAccountMenu
    {
        private IBankAccountFacade _bankAccountFacade;

        public BankAccountMenu(IBankAccountFacade bankAccountFacade)
        {
            _bankAccountFacade = bankAccountFacade;
        }

        public void Show()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Управление счетами:[/]")
                    .AddChoices(new[] {
                        "➕ Создать счет",
                        "📝 Изменить название счета",
                        "🗑️ Удалить счет",
                        "📋 Список счетов",
                        "🔙 Назад"
                    }));

            switch (choice)
            {
                case "➕ Создать счет":
                    CreateBankAccount();
                    break;
                case "📝 Изменить название счета":
                    ChangeBankAccountName();
                    break;
                case "🗑️ Удалить счет":
                    DeleteBankAccount();
                    break;
                case "📋 Список счетов":
                    ShowBankAccounts();
                    break;
            }
        }

        public void CreateBankAccount()
        {
            AnsiConsole.MarkupLine("[yellow]➕ СОЗДАНИЕ СЧЕТА[/]");

            var name = AnsiConsole.Ask<string>("[white]Название счета:[/]");

            var result = _bankAccountFacade.Create(new BankAccountCreateRequest(name));

            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Счет создан успешно![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        public void ChangeBankAccountName()
        {
            var accountsResult = _bankAccountFacade.GetAll();
            if (!accountsResult.IsSuccess || !accountsResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет счетов для изменения названия[/]");
                WaitForKey();
                return;
            }

            var accountNames = accountsResult.Data
                .Select(a => a.Name)
                .ToList();
            accountNames.Add("🔙 Назад");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите счет для изменения названия:[/]")
                    .AddChoices(accountNames));

            if (choice == "🔙 Назад") return;

            var name = AnsiConsole.Ask<string>("[white]Название счета:[/]");

            var result = _bankAccountFacade.ChangeName(choice, name);
            if (result.IsSuccess)
            {
                AnsiConsole.MarkupLine("[green]✓ Название счета изменено![/]");
            }
            else
            {
                AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
            }

            WaitForKey();
        }

        public void DeleteBankAccount()
        {
            var accountsResult = _bankAccountFacade.GetAll();
            if (!accountsResult.IsSuccess || !accountsResult.Data.Any())
            {
                AnsiConsole.MarkupLine("[red]Нет счетов для удаления[/]");
                WaitForKey();
                return;
            }

            var accountNames = accountsResult.Data
                .Select(a => $"{a.Name} ([grey]{a.Balance:C}[/])")
                .ToList();
            accountNames.Add("🔙 Назад");

            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Выберите счет для удаления:[/]")
                    .AddChoices(accountNames));

            if (choice == "🔙 Назад") return;

            var selectedAccount = accountsResult.Data.ToList()[
                accountNames.IndexOf(choice)
            ];

            if (AnsiConsole.Confirm($"[red]Удалить счет '{selectedAccount.Name}'?[/]", false))
            {
                var result = _bankAccountFacade.Delete(selectedAccount.Id);
                if (result.IsSuccess)
                {
                    AnsiConsole.MarkupLine("[green]✓ Счет удален![/]");
                }
                else
                {
                    AnsiConsole.MarkupLine($"[red]✗ {result.Message}[/]");
                }
            }

            WaitForKey();
        }

        public void ShowBankAccounts()
        {
            var result = _bankAccountFacade.GetAll();
            if (!result.IsSuccess || !result.Data.Any())
            {
                AnsiConsole.MarkupLine("[yellow]Нет созданных счетов[/]");
                WaitForKey();
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Title("[yellow]📋 ВАШИ СЧЕТА[/]");

            table.AddColumn("Название");
            table.AddColumn("Баланс");

            foreach (var account in result.Data)
            {
                var balanceColor = account.Balance >= 0 ? "green" : "red";
                table.AddRow(
                    account.Name,
                    $"[{balanceColor}]{account.Balance:C}[/]"
                );
            }

            AnsiConsole.Write(table);
            WaitForKey();
        }
    }
}
