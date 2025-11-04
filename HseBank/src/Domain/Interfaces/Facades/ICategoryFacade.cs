using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для работы с категориями.
    /// </summary>
    public interface ICategoryFacade
    {
        /// <summary>
        /// Создание категории.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания данными.</param>
        /// <returnsРезультат, содержащий статус выполнения и сообщение.></returns>
        public OperationResult Create(CategoryCreateRequest request);

        /// <summary>
        /// Получение всех категорий.
        /// </summary>
        /// <returns>Результат, содержащий статус выполнения, все категории и сообщение.</returns>
        public OperationResult<IEnumerable<Category>> GetAll();

        /// <summary>
        /// Получение категорий по определенному типу.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <returns>Результат, содержащий статус выполнения, категории определенного типа и сообщение.</returns>
        public OperationResult<IEnumerable<Category>> GetByType(TransactionType type);

        /// <summary>
        /// Изменение названия категории.
        /// </summary>
        /// <param name="oldName">Название категории, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult ChangeName(string oldName, string newName);

        /// <summary>
        /// Удаление категории.
        /// </summary>
        /// <param name="id">Айди категории.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Delete(Guid id);
    }
}
