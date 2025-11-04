namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для взаимодействия пользователя с историей команд.
    /// </summary>
    public interface IUndoRedoMenu
    {
        /// <summary>
        /// Отмена последней команды.
        /// </summary>
        public void UndoLastAction();

        /// <summary>
        /// Повтор отмененной команды.
        /// </summary>
        public void RedoAction();
    }
}
