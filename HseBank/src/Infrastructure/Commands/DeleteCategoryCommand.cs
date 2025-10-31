using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class DeleteCategoryCommand : ICommand
    {
        private string _name;
        private TransactionType _transactionType;
        private ICategoryFactory _categoryFactory;
        private ICategoryRepository _categoryRepository;

        public DeleteCategoryCommand(string name, ICategoryFactory categoryFactory, ICategoryRepository categoryRepository)
        {
            _name = name;
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
        }

        public void Execute()
        {
            var category = _categoryRepository.GetByName(_name);
            if (category is not null)
            {
                _transactionType = category.Type;
                _categoryRepository.Delete(category);
            }
        }

        public void Undo()
        {
            var category = _categoryRepository.GetByName(_name);
            if (category is null)
            {
                var request = new CategoryCreateRequest(_transactionType, _name);
                category = _categoryFactory.Create(request);
                _categoryRepository.Add(category);
            }
        }
    }
}
