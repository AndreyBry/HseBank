namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Определяет контракт для консольного меню.
    /// </summary>
    public interface IMenu
    {
        /// <summary>
        /// Отображение пунктов меню и выбор пользователем необходимого пункта.
        /// </summary>
        public void Show();
    }
}
