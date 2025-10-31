using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface IUndoFacade
    {
        public OperationResult Undo();
    }
}
