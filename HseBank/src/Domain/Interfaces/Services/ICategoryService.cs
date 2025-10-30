using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface ICategoryService
    {
        public Category CreateCategory(TransactionType type, string name);
        public void DeleteCategory(Category category);
    }
}
