namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для выбора пользователем типа импорта данных из файла.
    /// </summary>
    public interface IImportMenu : IMenu
    {
        /// <summary>
        /// Импорт данных из JSON-файла.
        /// </summary>
        public void ImportFromJson();

        /// <summary>
        /// Импорт данных из CSV-файла.
        /// </summary>
        public void ImportFromCsv();
    }
}
