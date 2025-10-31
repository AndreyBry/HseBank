using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    public class BankAccountRepository : IBankAccountRepository
    {
        private readonly Dictionary<Guid, BankAccount> _accounts = new();
        private readonly Dictionary<string, BankAccount> _accountsByName = new();

        public void Add(BankAccount bankAccount)
        {
            _accounts[bankAccount.Id] = bankAccount;
            _accountsByName[bankAccount.Name] = bankAccount;
        }

        public BankAccount? GetById(Guid bankId)
        {
            return _accounts.ContainsKey(bankId) ? _accounts[bankId] : null;
        }

        public BankAccount? GetByName(string name)
        {
            return _accountsByName.ContainsKey(name) ? _accountsByName[name] : null;
        }

        public void Update(BankAccount bankAccount)
        {
            if (_accounts.ContainsKey(bankAccount.Id))
            {
                _accounts[bankAccount.Id] = bankAccount;
                _accountsByName[bankAccount.Name] = bankAccount;
            }
        }

        public void Delete(BankAccount bankAccount)
        {
            _accounts.Remove(bankAccount.Id);
            _accountsByName.Remove(bankAccount.Name);
        }
    }
}
