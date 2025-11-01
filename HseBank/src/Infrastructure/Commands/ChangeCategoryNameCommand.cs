using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class ChangeCategoryNameCommand : ICommand
    {
        string _oldName;
        string _newName;
        private ICategoryRepository _categoryRepository;

        public ChangeCategoryNameCommand(string oldName, string newName, ICategoryRepository categoryRepository)
        {
            _oldName = oldName;
            _newName = newName;
            _categoryRepository = categoryRepository;
        }

        public void Execute()
        {
            var category = _categoryRepository.GetByName(_oldName);
            if (category is null)
            {
                throw new EntityNotFoundException("категория", _oldName);
            }
            category.UpdateName(_newName);
            _categoryRepository.Update(category);
        }

        public void Undo()
        {
            var category = _categoryRepository.GetByName(_newName);
            if (category is not null)
            {
                category.UpdateName(_oldName);
                _categoryRepository.Update(category);
            }
        }
    }
}
