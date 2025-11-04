using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    /// <summary>
    /// Репозиторий категорий в памяти.
    /// </summary>
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Dictionary<Guid, Category> _categories = new();

        /// <summary>
        /// Добавление категории.
        /// </summary>
        /// <param name="category">Категория.</param>
        public void Add(Category category)
        {
            if (_categories.ContainsKey(category.Id))
            {
                throw new ValidationException("Категория с таким айди уже существует.");
            }
            _categories[category.Id] = category;
        }

        /// <summary>
        /// Получение категории по айди.
        /// </summary>
        /// <param name="categoryId">Айди.</param>
        /// <returns>Категория.</returns>
        public Category? GetById(Guid categoryId)
        {
            return _categories.ContainsKey(categoryId) ? _categories[categoryId] : null;
        }

        /// <summary>
        /// Получение категории по названию.
        /// </summary>
        /// <param name="name">Название.</param>
        /// <returns>Категория.</returns>
        public Category? GetByName(string name)
        {
            List<Category> suitableCategories = _categories.Values.Where(c => c.Name == name).ToList();
            return suitableCategories.Count > 0 ? suitableCategories[0] : null;
        }

        /// <summary>
        /// Получение категорий определенного типа.
        /// </summary>
        /// <param name="type">Тип.</param>
        /// <returns>Категории определенного типа.</returns>
        public IEnumerable<Category> GetByType(TransactionType type)
        {
            return _categories.Values.Where(c => c.Type == type);
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <returns>Категории.</returns>
        public IEnumerable<Category> GetAll()
        {
            return _categories.Values;
        }

        /// <summary>
        /// Обновление данных о категории.
        /// </summary>
        /// <param name="category">Категория.</param>
        public void Update(Category category)
        {
            if (_categories.ContainsKey(category.Id))
            {
                _categories[category.Id] = category;
            }
        }

        /// <summary>
        /// Удаление категории.
        /// </summary>
        /// <param name="category">Категория.</param>
        public void Delete(Category category)
        {
            _categories.Remove(category.Id);
        }
    }
}
