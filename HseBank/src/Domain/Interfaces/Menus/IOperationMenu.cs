using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Menus
{
    /// <summary>
    /// Предоставляет методы для выбора пользователем действий, связанных с операциями, и ввода необходимых данных.
    /// </summary>
    public interface IOperationMenu : IMenu
    {
        /// <summary>
        /// Добавление операции.
        /// </summary>
        /// <param name="type">Тип операции (доход/расход).</param>
        public void AddOperation(TransactionType type);
    }
}
