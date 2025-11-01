using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository : IRepository<Category>
    {
        public Category? GetByName(string name);
        public IEnumerable<Category> GetByType(TransactionType type);
        public IEnumerable<Category> GetAll();
    }
}
