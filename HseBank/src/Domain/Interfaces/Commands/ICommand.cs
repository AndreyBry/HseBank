namespace HseBank.src.Domain.Interfaces.Commands
{
    /// <summary>
    /// Определяет контракт для команд.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Выполнение команды.
        /// </summary>
        public void Execute();

        /// <summary>
        /// Отмена действия команды.
        /// </summary>
        public void Undo();
    }
}
