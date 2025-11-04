using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// Репозиторий банковских счетов в памяти.
    /// </summary>
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly Dictionary<Guid, BankAccount> _accounts = new();

        /// <summary>
        /// Добавление счета.
        /// </summary>
        /// <param name="bankAccount">Счет.</param>
        public void Add(BankAccount bankAccount)
        {
            if (_accounts.ContainsKey(bankAccount.Id))
            {
                throw new ValidationException("Счет с таким айди уже существует.");
            }
            _accounts[bankAccount.Id] = bankAccount;
        }

        /// <summary>
        /// Получение счета по айди.
        /// </summary>
        /// <param name="bankId">йди.</param>
        /// <returns>Счет.</returns>
        public BankAccount? GetById(Guid bankId)
        {
            return _accounts.ContainsKey(bankId) ? _accounts[bankId] : null;
        }

        /// <summary>
        /// Получение счета по названию.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <returns>Счет.</returns>
        public BankAccount? GetByName(string name)
        {
            List<BankAccount> suitableAccounts = _accounts.Values.Where(a => a.Name == name).ToList();
            return suitableAccounts.Count > 0 ? suitableAccounts[0] : null;
        }

        /// <summary>
        /// Получение всех счетов.
        /// </summary>
        /// <returns>Счеты.</returns>
        public IEnumerable<BankAccount> GetAll()
        {
            return _accounts.Values;
        }

        /// <summary>
        /// Обновление данных о счете.
        /// </summary>
        /// <param name="bankAccount">Счет.</param>
        public void Update(BankAccount bankAccount)
        {
            if (_accounts.ContainsKey(bankAccount.Id))
            {
                _accounts[bankAccount.Id] = bankAccount;
            }
        }

        /// <summary>
        /// Удаление счета.
        /// </summary>
        /// <param name="bankAccount">Счет.</param>
        public void Delete(BankAccount bankAccount)
        {
            _accounts.Remove(bankAccount.Id);
        }
    }
}
