using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        private Guid _id;
        private string _name = string.Empty;
        private TransactionType _transactionType;
        private ICategoryFactory _categoryFactory;
        private ICategoryRepository _categoryRepository;

        public DeleteCategoryCommand(Guid id, ICategoryFactory categoryFactory, ICategoryRepository categoryRepository)
        {
            _id = id;
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
        }

        public void Execute()
        {
            var category = _categoryRepository.GetById(_id);
            if (category is null)
            {
                throw new EntityNotFoundException("категория", _id);
            }
            _name = category.Name;
            _transactionType = category.Type;
            _categoryRepository.Delete(category);
        }

        public void Undo()
        {
            var category = _categoryRepository.GetById(_id);
            if (category is null && _name != string.Empty)
            {
                var request = new CategoryCreateRequest(_transactionType, _name);
                category = _categoryFactory.Create(request);
                _categoryRepository.Add(category);
            }
        }
    }
}
