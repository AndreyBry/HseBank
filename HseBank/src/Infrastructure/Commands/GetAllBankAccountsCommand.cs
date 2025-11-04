using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда получения всех банковских счетов.
    /// </summary>
    public class GetAllBankAccountsCommand : IQueryCommand<IEnumerable<BankAccount>>
    {
        private IBankAccountRepository _bankAccountRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        public GetAllBankAccountsCommand(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Получение всех банковских счетов.
        /// </summary>
        /// <returns>Счеты.</returns>
        public IEnumerable<BankAccount> Execute()
        {
            var accounts = _bankAccountRepository.GetAll();
            return accounts;
        }
    }
}
