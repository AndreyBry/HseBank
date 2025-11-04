using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Services;
using System.Diagnostics;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Декоратор для измерения времени выполнения команды.
    /// </summary>
    public class TimedCommandDecorator : ICommand
    {
        private ICommand _decoratedCommand;
        private IMetricsService _metricsService;
        
        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="decoratedCommand">Команда, время выполнения которой необходимо измерить.</param>
        /// <param name="metricsService">Сервис, содержащий метрики выполнения всех команд.</param>
        public TimedCommandDecorator(ICommand decoratedCommand, IMetricsService metricsService)
        {
            _decoratedCommand = decoratedCommand;
            _metricsService = metricsService;
        }

        /// <summary>
        /// Измерение времени выполнения команды.
        /// </summary>
        public void Execute()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _decoratedCommand.Execute();
                stopwatch.Stop();
                ReportMetrics(stopwatch.Elapsed, true);
            }
            catch (Exception)
            {
                stopwatch.Stop();
                ReportMetrics(stopwatch.Elapsed, false);
                throw;
            }
        }

        /// <summary>
        /// Отмена команды.
        /// </summary>
        public void Undo()
        {
            _decoratedCommand.Undo();
        }

        private string CommandName => _decoratedCommand.GetType().ToString().Split('.')[^1];

        /// <summary>
        /// Сохранение метрик выполнения команды в сервисе метрик.
        /// </summary>
        /// <param name="elapsed">Время выполнения.</param>
        /// <param name="successful">Успешность выполнения.</param>
        private void ReportMetrics(TimeSpan elapsed, bool successful)
        {
            _metricsService.RecordCommandExecution(CommandName, elapsed, successful);
        }
    }
}
