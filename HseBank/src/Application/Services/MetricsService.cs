using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Metrics;
using System.Collections.Concurrent;

namespace HseBank.src.Application.Services
{
    /// <summary>
    /// Класс, содержащий метрики выполнения команд.
    /// </summary>
    public class MetricsService : IMetricsService
    {
        private readonly ConcurrentDictionary<string, CommandMetrics> _metrics = new();

        /// <summary>
        /// Метод для записи метрик выполнения команды.
        /// </summary>
        /// <param name="commandType">Название команды.</param>
        /// <param name="duration">Время выполнения.</param>
        /// <param name="successful">Успешность выполнения.</param>
        public void RecordCommandExecution(string commandType, TimeSpan duration, bool successful)
        {
            var metrics = _metrics.GetOrAdd(commandType, _ => new CommandMetrics { CommandType = commandType });
            metrics.TotalExecutions++;
            metrics.TotalDuration += duration;
            if (successful)
            {
                metrics.SuccessfulExecutions++;
            }
            else
            {
                metrics.FailedExecutions++;
            }
            if (duration > metrics.MaxDuration)
            {
                metrics.MaxDuration = duration;
            }
            if (duration < metrics.MinDuration || metrics.MinDuration == TimeSpan.Zero)
            {
                metrics.MinDuration = duration;
            }
        }

        /// <summary>
        /// Метод для получения всех метрик.
        /// </summary>
        /// <returns>Метрики выполнения команд.</returns>
        public IEnumerable<CommandMetrics> GetAllMetrics()
        {
            return _metrics.Values.Select(m => m.Clone()).OrderByDescending(m => m.TotalExecutions);
        }

        /// <summary>
        /// Метод для очистки метрик.
        /// </summary>
        public void ClearMetrics()
        {
            _metrics.Clear();
        }
    }
}
