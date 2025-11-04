using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    /// <summary>
    /// Фабрика категорий.
    /// </summary>
    public class CategoryFactory : ICategoryFactory
    {
        /// <summary>
        /// Создание корректной категории.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания категории данными.</param>
        /// <returns>Категория.</returns>
        /// <exception cref="ValidationException">Выбрасывается, если валидация не пройдена.</exception>
        public Category Create(CategoryCreateRequest request)
        {
            if (request.name.Length == 0 || request.name.Length > 20)
            {
                throw new ValidationException("Название категории может содержать минимум 1 и максимум 20 символов.");
            }
            Guid id = request.id ?? Guid.NewGuid();
            return new Category(id, request.type, request.name);
        }
    }
}
