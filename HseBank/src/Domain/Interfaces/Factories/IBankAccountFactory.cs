using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface IBankAccountFactory : IFactory<BankAccount, BankAccountCreateRequest>
    {
    }
}
