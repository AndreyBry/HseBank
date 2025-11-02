using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Metrics;
using Spectre.Console;
using System.Text;

namespace HseBank.src.UserInterface.Menus
{
    public class MetricsMenu : MenuExtensions, IMetricsMenu
    {
        private IMetricsService _metricsService;

        public MetricsMenu(IMetricsService metricsService)
        {
            _metricsService = metricsService;
        }

        public void Show()
        {
            var choice = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("[white]Метрики производительности:[/]")
                    .AddChoices(new[] {
                        "📈 Показать метрики",
                        "🗑️ Очистить метрики",
                        "🔙 Назад"
                    }));

            switch (choice)
            {
                case "📈 Показать метрики":
                    ShowCommandMetrics();
                    break;
                case "🗑️ Очистить метрики":
                    ClearMetrics();
                    break;
            }
        }

        public void ShowCommandMetrics()
        {
            var allMetrics = _metricsService.GetAllMetrics().ToList();

            if (!allMetrics.Any())
            {
                AnsiConsole.MarkupLine("[yellow]📭 Метрики еще не собраны.[/]");
                WaitForKey();
                return;
            }

            var table = new Table();
            table.Border(TableBorder.Rounded);
            table.Title("[bold blue]📊 Метрики выполнения команд[/]");

            table.AddColumn(new TableColumn("[bold]Команда[/]").LeftAligned());
            table.AddColumn(new TableColumn("[bold]Выполнено[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Успешно[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Ср. время[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Мин.[/]").Centered());
            table.AddColumn(new TableColumn("[bold]Макс.[/]").Centered());

            foreach (var metrics in allMetrics)
            {
                var successColor = metrics.SuccessRate >= 90 ? "green" : metrics.SuccessRate >= 80 ? "yellow" : "red";
                var speedColor = metrics.AverageDuration.TotalMilliseconds < 100 ? "green" :
                                metrics.AverageDuration.TotalMilliseconds < 500 ? "yellow" : "red";

                table.AddRow(
                    $"[bold]{metrics.CommandType}[/]",
                    $"[cyan]{metrics.TotalExecutions}[/]",
                    $"[{successColor}]{metrics.SuccessRate:0.0}%[/]",
                    $"[{speedColor}]{metrics.AverageDuration.TotalMilliseconds:0.000} мс[/]",
                    $"[grey]{metrics.MinDuration.TotalMilliseconds:0} мс[/]",
                    $"[grey]{metrics.MaxDuration.TotalMilliseconds:0} мс[/]"
                );
            }

            AnsiConsole.Write(table);

            ShowPerformanceAnalysis(allMetrics);

            WaitForKey();
        }

        public void ClearMetrics()
        {
            var allMetrics = _metricsService.GetAllMetrics().ToList();

            if (!allMetrics.Any())
            {
                AnsiConsole.MarkupLine("[yellow]📭 Метрики еще не собраны.[/]");
                WaitForKey();
                return;
            }

            var confirm = AnsiConsole.Confirm(
                "[red]⚠️ Вы уверены, что хотите очистить все метрики?[/]",
                false);

            if (confirm)
            {
                _metricsService.ClearMetrics();
                AnsiConsole.MarkupLine("[green]✅ Метрики успешно очищены![/]");
            }
            else
            {
                AnsiConsole.MarkupLine("[yellow]❌ Очистка отменена[/]");
            }

            WaitForKey();
        }

        private void ShowPerformanceAnalysis(List<CommandMetrics> metrics)
        {
            Console.WriteLine();

            var panel = new Panel(GetAnalysisText(metrics))
                .Header("[bold blue]💡 Анализ производительности[/]")
                .Border(BoxBorder.Rounded);

            AnsiConsole.Write(panel);
        }

        private string GetAnalysisText(List<CommandMetrics> metrics)
        {
            var slowest = metrics.OrderByDescending(m => m.AverageDuration.TotalMilliseconds).First();
            var mostUsed = metrics.OrderByDescending(m => m.TotalExecutions).First();
            var mostReliable = metrics.Where(m => m.TotalExecutions >= 3)
                                     .OrderByDescending(m => m.SuccessRate)
                                     .FirstOrDefault();

            var analysis = new StringBuilder();

            analysis.AppendLine($"[red]🐌 Самая медленная:[/] {slowest.CommandType} ({slowest.AverageDuration.TotalMilliseconds:0} мс)");
            analysis.AppendLine($"[cyan]🔥 Самая популярная:[/] {mostUsed.CommandType} ({mostUsed.TotalExecutions} раз)");

            if (mostReliable != null)
            {
                analysis.AppendLine($"[green]✅ Самая надежная:[/] {mostReliable.CommandType} ({mostReliable.SuccessRate:0.0}% успеха)");
            }

            return analysis.ToString();
        }
    }
}
