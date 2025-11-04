using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для создания категории.
    /// </summary>
    public class CreateCategoryCommand : ICommand
    {
        private CategoryCreateRequest _request;
        private ICategoryFactory _categoryFactory;
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания категории данными.</param>
        /// <param name="categoryFactory">Фабрика категорий.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public CreateCategoryCommand(CategoryCreateRequest request, ICategoryFactory categoryFactory, ICategoryRepository categoryRepository)
        {
            _request = request;
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Создание категории и сохранение в репозитории.
        /// </summary>
        public void Execute()
        {
            var category = _categoryFactory.Create(_request);
            _categoryRepository.Add(category);
        }

        /// <summary>
        /// Удаление созданной категории.
        /// </summary>
        public void Undo()
        {
            var category = _categoryRepository.GetByName(_request.name);
            if (category is not null)
            {
                _categoryRepository.Delete(category);
            }
        }
    }
}
