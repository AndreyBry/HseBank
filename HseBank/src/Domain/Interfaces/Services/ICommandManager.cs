using HseBank.src.Domain.Interfaces.Commands;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface ICommandManager
    {
        public void Execute(ICommand command);
        public T Execute<T>(IQueryCommand<T> command);
        public void Undo();
        public void Redo();
        public bool CanUndo { get; }
        public bool CanRedo { get; }
    }
}
