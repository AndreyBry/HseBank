using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем банковских счетов.
    /// </summary>
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        /// <summary>
        /// Получение счета по названию.
        /// </summary>
        /// <param name="name">Название счета.</param>
        /// <returns>Счет.</returns>
        public BankAccount? GetByName(string name);
    }
}
