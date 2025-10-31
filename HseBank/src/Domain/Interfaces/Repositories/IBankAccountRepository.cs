using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IBankAccountRepository : IRepository<BankAccount>
    {
        BankAccount? GetByName(string name);
    }
}
