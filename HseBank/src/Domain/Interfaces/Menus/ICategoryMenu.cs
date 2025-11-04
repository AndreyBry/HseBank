namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для выбора пользователем действий, связанных с категориями, и ввода необходимых данных.
    /// </summary>
    public interface ICategoryMenu : IMenu
    {
        /// <summary>
        /// Создание категории.
        /// </summary>
        public void CreateCategory();

        /// <summary>
        /// Изменение названия категории.
        /// </summary>
        public void ChangeCategoryName();

        /// <summary>
        /// Удаление категории.
        /// </summary>
        public void DeleteCategory();

        /// <summary>
        /// Отображение всех категорий.
        /// </summary>
        public void ShowCategories();
    }
}
