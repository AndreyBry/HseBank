using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для получения категорий определенного типа.
    /// </summary>
    public class GetCategoriesByTypeCommand : IQueryCommand<IEnumerable<Category>>
    {
        private TransactionType _transactionType;
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public GetCategoriesByTypeCommand(TransactionType type, ICategoryRepository categoryRepository)
        {
            _transactionType = type;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Получение категорий определенного типа.
        /// </summary>
        /// <returns>Категории определенного типа.</returns>
        public IEnumerable<Category> Execute()
        {
            var categories = _categoryRepository.GetByType(_transactionType);
            return categories;
        }
    }
}
