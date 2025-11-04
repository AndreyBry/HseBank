namespace HseBank.src.Domain.Models.Metrics
{
    /// <summary>
    /// Содержит метрики выполнения команды.
    /// </summary>
    public class CommandMetrics
    {
        public string CommandType { get; set; }
        public int TotalExecutions { get; set; }
        public int SuccessfulExecutions { get; set; }
        public int FailedExecutions { get; set; }
        public TimeSpan TotalDuration { get; set; }
        public TimeSpan MaxDuration { get; set; }
        public TimeSpan MinDuration { get; set; }

        public TimeSpan AverageDuration => TotalExecutions > 0
            ? TimeSpan.FromMilliseconds(TotalDuration.TotalMilliseconds / TotalExecutions)
            : TimeSpan.Zero;

        public double SuccessRate => TotalExecutions > 0
            ? (SuccessfulExecutions * 100.0) / TotalExecutions
            : 0;

        /// <summary>
        /// Клонирование метрик.
        /// </summary>
        /// <returns>Метрики выполнения команды.</returns>
        public CommandMetrics Clone()
        {
            return new CommandMetrics
            {
                CommandType = CommandType,
                TotalExecutions = TotalExecutions,
                SuccessfulExecutions = SuccessfulExecutions,
                FailedExecutions = FailedExecutions,
                TotalDuration = TotalDuration,
                MaxDuration = MaxDuration,
                MinDuration = MinDuration
            };
        }
    }
}
