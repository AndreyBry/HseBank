using HseBank.src.Domain.Interfaces.Commands;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface ICommandManager
    {
        public void Execute(ICommand command);
        public void Undo();
        public bool CanUndo { get; }
    }
}
