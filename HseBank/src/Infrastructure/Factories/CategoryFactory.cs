using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Factories;

namespace HseBank.src.Infrastructure.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(TransactionType type, string name)
        {
            if (name.Length == 0 || name.Length > 20)
            {
                throw new ArgumentException("Название категории может содержать минимум 1 и максимум 20 символов.");
            }
            Guid id = Guid.NewGuid();
            return new Category(id, type, name);
        }
    }
}
