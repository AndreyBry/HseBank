using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    public class CategoryFactory : ICategoryFactory
    {
        public Category Create(CategoryCreateRequest request)
        {
            if (request.name.Length == 0 || request.name.Length > 20)
            {
                throw new ValidationException("Название категории может содержать минимум 1 и максимум 20 символов.");
            }
            Guid id = Guid.NewGuid();
            return new Category(id, request.type, request.name);
        }
    }
}
