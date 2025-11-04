namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для просмотра пользователем метрик выполнения команд.
    /// </summary>
    public interface IMetricsMenu : IMenu
    {
        /// <summary>
        /// Отображение всех метрик выполнения команд.
        /// </summary>
        public void ShowCommandMetrics();

        /// <summary>
        /// Очистка метрик.
        /// </summary>
        public void ClearMetrics();
    }
}
