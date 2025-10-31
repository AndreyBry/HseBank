using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    public class BankAccountFactory : IBankAccountFactory
    {
        public BankAccount Create(BankAccountCreateRequest request)
        {
            if (request.name.Length == 0 || request.name.Length > 15)
            {
                throw new ValidationException("Название счета может содержать минимум 1 и максимум 15 символов.");
            }
            Guid id = Guid.NewGuid();
            decimal balance = 0;
            return new BankAccount(id, request.name, balance);
        }
    }
}
