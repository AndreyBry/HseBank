using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface IUndoRedoFacade
    {
        public OperationResult Undo();
        public OperationResult Redo();
    }
}
