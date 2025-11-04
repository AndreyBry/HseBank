using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Factories
{
    /// <summary>
    /// Предоставляет методы для корректного создания объектов.
    /// </summary>
    /// <typeparam name="TEntity">Тип создаваемого объекта.</typeparam>
    /// <typeparam name="TCreateRequest">ДТО с необходимыми для создания данными.</typeparam>
    public interface IFactory<TEntity, TCreateRequest> where TEntity : EntityBase
    {
        /// <summary>
        /// Создание объекта.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания данными.</param>
        /// <returns>Созданный объект.</returns>
        public TEntity Create(TCreateRequest request);
    }
}
