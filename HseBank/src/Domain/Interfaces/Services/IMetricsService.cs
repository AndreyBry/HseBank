using HseBank.src.Domain.Models.Metrics;

namespace HseBank.src.Domain.Interfaces.Services
{
    /// <summary>
    /// Предоставляет методы для хранения и получения метрик выполнения всех команд.
    /// </summary>
    public interface IMetricsService
    {
        /// <summary>
        /// Сохранение метрик выполнения команды.
        /// </summary>
        /// <param name="commandType">Название команды.</param>
        /// <param name="duration">Время выполнения.</param>
        /// <param name="successful">Успешность выполнения.</param>
        public void RecordCommandExecution(string commandType, TimeSpan duration, bool successful);

        /// <summary>
        /// Получение метрик выполнения всех команд.
        /// </summary>
        /// <returns>Метрики.</returns>
        public IEnumerable<CommandMetrics> GetAllMetrics();

        /// <summary>
        /// Очистка хранилища метрик выполнения команд.
        /// </summary>
        public void ClearMetrics();
    }
}
