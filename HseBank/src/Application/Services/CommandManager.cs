using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Services;

namespace HseBank.src.Application.Services
{
    public class CommandManager : ICommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();
        private readonly Stack<ICommand> _redoStack = new();

        public bool CanUndo => _undoStack.Count > 0;
        public bool CanRedo => _redoStack.Count > 0;

        public void Execute(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
            _redoStack.Clear();
        }

        public T Execute<T>(IQueryCommand<T> command)
        {
            T result = command.Execute();
            return result;
        }

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
