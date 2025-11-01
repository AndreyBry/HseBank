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

        public BankAccount? GetByName(string name)
        {
            List<BankAccount> suitableAccounts = _accounts.Values.Where(a => a.Name == name).ToList();
            return suitableAccounts.Count > 0 ? suitableAccounts[0] : null;
        }

        public IEnumerable<BankAccount> GetAll()
        {
            return _accounts.Values;
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
