using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly Dictionary<Guid, BankAccount> _accounts = new();

        public void Add(BankAccount bankAccount)
        {
            _accounts[bankAccount.Id] = bankAccount;
        }

        public BankAccount? GetById(Guid bankId)
        {
            return _accounts.ContainsKey(bankId) ? _accounts[bankId] : null;
        }

        public void Update(BankAccount bankAccount)
        {
            if (_accounts.ContainsKey(bankAccount.Id))
            {
                _accounts[bankAccount.Id] = bankAccount;
            }
        }

        public void Delete(BankAccount bankAccount)
        {
            _accounts.Remove(bankAccount.Id);
        }
    }
}
