using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Repositories.InMemory
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly Dictionary<Guid, Category> _categories = new();
        private readonly Dictionary<string, Category> _categoriesByName = new();

        public void Add(Category category)
        {
            _categories[category.Id] = category;
            _categoriesByName[category.Name] = category;
        }

        public Category? GetById(Guid categoryId)
        {
            return _categories.ContainsKey(categoryId) ? _categories[categoryId] : null;
        }

        public Category? GetByName(string name)
        {
            return _categoriesByName.ContainsKey(name) ? _categoriesByName[name] : null;
        }

        public void Update(Category category)
        {
            if (_categories.ContainsKey(category.Id))
            {
                _categories[category.Id] = category;
                _categoriesByName[category.Name] = category;
            }
        }

        public void Delete(Category category)
        {
            _categories.Remove(category.Id);
            _categoriesByName.Remove(category.Name);
        }
    }
}
