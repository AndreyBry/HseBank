using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    public class CreateCategoryCommand : ICommand
    {
        private CategoryCreateRequest _request;
        private ICategoryFactory _categoryFactory;
        private ICategoryRepository _categoryRepository;

        public CreateCategoryCommand(CategoryCreateRequest request, ICategoryFactory categoryFactory, ICategoryRepository categoryRepository)
        {
            _request = request;
            _categoryFactory = categoryFactory;
            _categoryRepository = categoryRepository;
        }

        public void Execute()
        {
            var category = _categoryFactory.Create(_request);
            _categoryRepository.Add(category);
        }

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
