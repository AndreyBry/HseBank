using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    /// <summary>
    /// Фабрика банковских счетов.
    /// </summary>
    public class BankAccountFactory : IBankAccountFactory
    {
        /// <summary>
        /// Создание корректного банковского счета.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания банковского счета данными.</param>
        /// <returns>Банковский счет.</returns>
        /// <exception cref="ValidationException">Выбрасывается, если валидация не пройдена.</exception>
        public BankAccount Create(BankAccountCreateRequest request)
        {
            if (request.name.Length == 0 || request.name.Length > 15)
            {
                throw new ValidationException("Название счета может содержать минимум 1 и максимум 15 символов.");
            }
            Guid id = request.id ?? Guid.NewGuid();
            decimal balance = request.balance;
            return new BankAccount(id, request.name, balance);
        }
    }
}
