using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class GetAllCategoriesCommand : IQueryCommand<IEnumerable<Category>>
    {
        private ICategoryRepository _categoryRepository;

        public GetAllCategoriesCommand(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> Execute()
        {
            var categories = _categoryRepository.GetAll();
            return categories;
        }
    }
}
