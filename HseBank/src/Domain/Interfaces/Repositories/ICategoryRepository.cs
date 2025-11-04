using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем категорий.
    /// </summary>
    public interface ICategoryRepository : IRepository<Category>
    {
        /// <summary>
        /// Получение категории по названию.
        /// </summary>
        /// <param name="name">Название категории.</param>
        /// <returns>Категория.</returns>
        public Category? GetByName(string name);
        
        /// <summary>
        /// Получение категорий по определенному типу.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <returns>Категории.</returns>
        public IEnumerable<Category> GetByType(TransactionType type);
    }
}
