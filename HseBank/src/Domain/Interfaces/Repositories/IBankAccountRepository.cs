using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface IBankAccountRepository
    {
        void Add(BankAccount bankAccount);
        BankAccount? GetById(Guid bankId);
        void Update(BankAccount bankAccount);
        void Delete(BankAccount bankAccount);
    }
}
