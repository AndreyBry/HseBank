using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface ICategoryFactory
    {
        public Category Create(TransactionType type, string name);
    }
}
