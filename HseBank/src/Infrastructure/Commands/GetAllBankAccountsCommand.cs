using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class GetAllBankAccountsCommand : IQueryCommand<IEnumerable<BankAccount>>
    {
        private IBankAccountRepository _bankAccountRepository;

        public GetAllBankAccountsCommand(IBankAccountRepository bankAccountRepository)
        {
            _bankAccountRepository = bankAccountRepository;
        }

        public IEnumerable<BankAccount> Execute()
        {
            var accounts = _bankAccountRepository.GetAll();
            return accounts;
        }
    }
}
