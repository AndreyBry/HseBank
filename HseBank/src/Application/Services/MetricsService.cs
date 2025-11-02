using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Metrics;
using System.Collections.Concurrent;

namespace HseBank.src.Application.Services
{
    public class MetricsService : IMetricsService
    {
        private readonly ConcurrentDictionary<string, CommandMetrics> _metrics = new();

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

        public IEnumerable<CommandMetrics> GetAllMetrics()
        {
            return _metrics.Values.Select(m => m.Clone()).OrderByDescending(m => m.TotalExecutions);
        }

        public void ClearMetrics()
        {
            _metrics.Clear();
        }
    }
}
