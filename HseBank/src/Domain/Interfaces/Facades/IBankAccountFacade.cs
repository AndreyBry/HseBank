using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для работы с банковскими счетами.
    /// </summary>
    public interface IBankAccountFacade
    {
        /// <summary>
        /// Создание счета.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания данными.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Create(BankAccountCreateRequest request);

        /// <summary>
        /// Получение всех счетов.
        /// </summary>
        /// <returns>Результат, содержащий статус выполнения, все счета и сообщение.</returns>
        public OperationResult<IEnumerable<BankAccount>> GetAll();

        /// <summary>
        /// Изменение названия счета.
        /// </summary>
        /// <param name="oldName">Название счета, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult ChangeName(string oldName, string newName);

        /// <summary>
        /// Удаление счета.
        /// </summary>
        /// <param name="id">Айди счета.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Delete(Guid id);
    }
}
