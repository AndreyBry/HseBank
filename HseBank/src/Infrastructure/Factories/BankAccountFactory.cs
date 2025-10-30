using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Factories;

namespace HseBank.src.Infrastructure.Factories
{
    public class BankAccountFactory : IBankAccountFactory
    {
        public BankAccount Create(string name)
        {
            if (name.Length == 0 || name.Length > 15)
            {
                throw new ArgumentException("Название счета может содержать минимум 1 и максимум 15 символов.");
            }
            Guid id = Guid.NewGuid();
            decimal balance = 0;
            return new BankAccount(id, name, balance);
        }
    }
}
