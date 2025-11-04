using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для изменения названия категории.
    /// </summary>
    public class ChangeCategoryNameCommand : ICommand
    {
        string _oldName;
        string _newName;
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="oldName">Название категории, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public ChangeCategoryNameCommand(string oldName, string newName, ICategoryRepository categoryRepository)
        {
            _oldName = oldName;
            _newName = newName;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Изменение названия категории.
        /// </summary>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если категория не существует.</exception>
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

        /// <summary>
        /// Возврат старого названия категории.
        /// </summary>
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
