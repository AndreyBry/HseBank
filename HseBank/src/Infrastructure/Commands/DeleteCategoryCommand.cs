using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для удаления категории.
    /// </summary>
    public class DeleteCategoryCommand : ICommand
    {
        private Guid _id;
        private string _name = string.Empty;
        private TransactionType _transactionType;
        private ICategoryFactory _categoryFactory;
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Айди категории.</param>
        /// <param name="categoryFactory">Фабрика категорий.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public DeleteCategoryCommand(Guid id, ICategoryFactory categoryFactory, ICategoryRepository categoryRepository)
        {
            _id = id;
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Удаление категории.
        /// </summary>
        /// <exception cref="EntityNotFoundException"></exception>
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

        /// <summary>
        /// Создание удаленной категории.
        /// </summary>
        public void Undo()
        {
            var category = _categoryRepository.GetById(_id);
            if (category is null && _name != string.Empty)
            {
                var request = new CategoryCreateRequest(_transactionType, _name, id: _id);
                category = _categoryFactory.Create(request);
                _categoryRepository.Add(category);
            }
        }
    }
}
