using HseBank.src.Domain.Models.Metrics;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface IMetricsService
    {
        public void RecordCommandExecution(string commandType, TimeSpan duration, bool successful);
        public IEnumerable<CommandMetrics> GetAllMetrics();
        public void ClearMetrics();
    }
}
