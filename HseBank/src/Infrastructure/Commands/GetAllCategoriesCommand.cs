using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для получения всех категорий.
    /// </summary>
    public class GetAllCategoriesCommand : IQueryCommand<IEnumerable<Category>>
    {
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public GetAllCategoriesCommand(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <returns>Категории.</returns>
        public IEnumerable<Category> Execute()
        {
            var categories = _categoryRepository.GetAll();
            return categories;
        }
    }
}
