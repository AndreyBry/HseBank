using HseBank.src.Domain.Interfaces.Commands;

namespace HseBank.src.Domain.Interfaces.Services
{
    /// <summary>
    /// Предоставляет методы для выполения команд, хранения истории их выполнения, отмены последней команды и повтора отмененной.
    /// </summary>
    public interface ICommandManager
    {
        /// <summary>
        /// Выполнение команды.
        /// </summary>
        /// <param name="command">Команда.</param>
        public void Execute(ICommand command);

        /// <summary>
        /// Выполнение команды-запроса.
        /// </summary>
        /// <typeparam name="T">Тип возвращаемого результата.</typeparam>
        /// <param name="command">Команда.</param>
        /// <returns>Результат выполнения команды.</returns>
        public T Execute<T>(IQueryCommand<T> command);

        /// <summary>
        /// Отмена последней команды.
        /// </summary>
        public void Undo();

        /// <summary>
        /// Повтор отмененной команды.
        /// </summary>
        public void Redo();

        public bool CanUndo { get; }
        public bool CanRedo { get; }
    }
}
