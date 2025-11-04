using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Repositories
{
    /// <summary>
    /// Предоставляет методы для работы с хранилищем объектов.
    /// </summary>
    /// <typeparam name="TEntity">Тип объекта.</typeparam>
    public interface IRepository<TEntity> where TEntity : EntityBase
    {
        /// <summary>
        /// Добавление объекта в хранилище.
        /// </summary>
        /// <param name="entity">Объект.</param>
        public  void Add(TEntity entity);

        /// <summary>
        /// Получение объекта из хранилища по айди.
        /// </summary>
        /// <param name="entityId">Айди объекта.</param>
        /// <returns>Объект.</returns>
        public TEntity? GetById(Guid entityId);

        /// <summary>
        /// Получение всех объектов из хранилища.
        /// </summary>
        /// <returns>Объекты.</returns>
        public IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Обновление данных об объекте в хранилище.
        /// </summary>
        /// <param name="entity">Объект.</param>
        public void Update(TEntity entity);

        /// <summary>
        /// Удаление объекта из хранилища.
        /// </summary>
        /// <param name="entity">Объект.</param>
        public void Delete(TEntity entity);
    }
}
