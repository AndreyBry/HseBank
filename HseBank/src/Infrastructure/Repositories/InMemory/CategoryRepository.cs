using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Dictionary<Guid, Category> _categories = new();

        public void Add(Category category)
        {
            _categories[category.Id] = category;
        }

        public Category? GetById(Guid categoryId)
        {
            return _categories.ContainsKey(categoryId) ? _categories[categoryId] : null;
        }

        public Category? GetByName(string name)
        {
            List<Category> suitableCategories = _categories.Values.Where(c => c.Name == name).ToList();
            return suitableCategories.Count > 0 ? suitableCategories[0] : null;
        }

        public IEnumerable<Category> GetByType(TransactionType type)
        {
            return _categories.Values.Where(c => c.Type == type);
        }

        public IEnumerable<Category> GetAll()
        {
            return _categories.Values;
        }

        public void Update(Category category)
        {
            if (_categories.ContainsKey(category.Id))
            {
                _categories[category.Id] = category;
            }
        }

        public void Delete(Category category)
        {
            _categories.Remove(category.Id);
        }
    }
}
