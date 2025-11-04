using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Export
{
    /// <summary>
    /// Предоставляет методы для экспорта данных в файл.
    /// </summary>
    public interface IDataVisitor
    {
        /// <summary>
        /// Сериализация счета.
        /// </summary>
        /// <param name="bankAccount">Счет.</param>
        void Visit(BankAccount bankAccount);

        /// <summary>
        /// Сериализация категории.
        /// </summary>
        /// <param name="category">Категория.</param>
        void Visit(Category category);

        /// <summary>
        /// Сериализация операции.
        /// </summary>
        /// <param name="operation">Операция.</param>
        void Visit(Operation operation);
        
        /// <summary>
        /// Получение результата сериализации всех объектов.
        /// </summary>
        /// <returns>Строка с сериализованными объектами.</returns>
        string GetResult();

        /// <summary>
        /// Сброс накопленного результата сериализации.
        /// </summary>
        void Reset();
    }
}
