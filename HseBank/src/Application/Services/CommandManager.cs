using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Services;

namespace HseBank.src.Application.Services
{
    public class CommandManager : ICommandManager
    {
        private readonly Stack<ICommand> _undoStack = new();

        public bool CanUndo => _undoStack.Count > 0;

        public void Execute(ICommand command)
        {
            command.Execute();
            _undoStack.Push(command);
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
            }
            catch (Exception)
            {
                _undoStack.Push(command);
                throw;
            }
        }
    }
}
