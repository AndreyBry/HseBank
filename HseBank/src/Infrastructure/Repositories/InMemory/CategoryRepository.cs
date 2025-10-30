using HseBank.src.Domain.Entities;
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
