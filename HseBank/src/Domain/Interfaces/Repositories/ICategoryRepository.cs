using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    public interface ICategoryRepository
    {
        void Add(Category category);
        Category? GetById(Guid categoryId);
        void Update(Category category);
        void Delete(Category category);
    }
}
