using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для отмены последнего действия и повтора отмененного действия.
    /// </summary>
    public interface IUndoRedoFacade
    {
        /// <summary>
        /// Отмена последнего действия.
        /// </summary>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Undo();

        /// <summary>
        /// Повтор отмененного действия.
        /// </summary>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Redo();
    }
}
