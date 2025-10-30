using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface IAccountService
    {
        public void CreateAccount(string name);
        public void DeleteAccount(BankAccount account);
        public void BlockAccount(BankAccount account);
        public void UnBlockAccount(BankAccount account);

        public decimal GetBalance(BankAccount account);
    }
}
