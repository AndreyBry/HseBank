using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface IBankAccountFactory
    {
        public BankAccount Create(string name);
    }
}
