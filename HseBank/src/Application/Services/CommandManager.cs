using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Services;

namespace HseBank.src.Application.Services
{
    /// <summary>
    /// Менеджер команд, производящий выполнение команд и позволяющий производить их отмену и повтор отмененных.
    /// </summary>
    public class CommandManager : ICommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        /// <summary>
        /// Метод для выполнения команды.
        /// </summary>
        /// <param name="command">Команда.</param>
        public void Execute(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        /// <summary>
        /// Метод для выполнения команды с возвращаемым результатом.
        /// </summary>
        /// <typeparam name="T">Тип данных результата.</typeparam>
        /// <param name="command">Команда.</param>
        /// <returns>Результат выполнения команды.</returns>
        public T Execute<T>(IQueryCommand<T> command)
        {
            T result = command.Execute();
            return result;
        }

        /// <summary>
        /// Метод для отмены последней команды.
        /// </summary>
        public void Undo()
        {
            if (!CanUndo)
            {
                return;
            }
            var command = _undoStack.Pop();
            try
            {
                command.Undo();
                _redoStack.Push(command);
            }
            catch (Exception)
            {
                _undoStack.Push(command);
                throw;
            }
        }

        /// <summary>
        /// Метод для повтора отмененной команды.
        /// </summary>
        public void Redo()
        {
            if (!CanRedo)
            {
                return;
            }
            var command = _redoStack.Pop();
            try
            {
                command.Execute();
                _undoStack.Push(command);
            }
            catch (Exception)
            {
                _redoStack.Push(command);
                throw;
            }
        }
    }
}
