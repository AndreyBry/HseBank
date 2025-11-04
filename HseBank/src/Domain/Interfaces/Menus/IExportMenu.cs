namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы выбора пользователем типа экспорта данных в файл.
    /// </summary>
    public interface IExportMenu : IMenu
    {
        /// <summary>
        /// Экспорт данных в JSON-файл.
        /// </summary>
        public void ExportToJson();

        /// <summary>
        /// Экспорт данных в CSV-файл.
        /// </summary>
        public void ExportToCsv();
    }
}
