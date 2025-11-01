using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        public BankAccount? GetByName(string name);
        public IEnumerable<BankAccount> GetAll();
    }
}
