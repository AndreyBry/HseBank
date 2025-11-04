using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    /// <summary>
    /// Предоставляет методы для корректного создания категорий.
    /// </summary>
    public interface ICategoryFactory : IFactory<Category, CategoryCreateRequest>
    {
    }
}
