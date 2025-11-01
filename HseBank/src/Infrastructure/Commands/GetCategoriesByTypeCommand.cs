using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class GetCategoriesByTypeCommand : IQueryCommand<IEnumerable<Category>>
    {
        private TransactionType _transactionType;
        private ICategoryRepository _categoryRepository;

        public GetCategoriesByTypeCommand(TransactionType type, ICategoryRepository categoryRepository)
        {
            _transactionType = type;
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> Execute()
        {
            var categories = _categoryRepository.GetByType(_transactionType);
            return categories;
        }
    }
}
