using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    /// <summary>
    /// Предоставляет методы для работы с операциями по счетам.
    /// </summary>
    public interface IOperationFacade
    {
        /// <summary>
        /// Создание и выполение операции по счету.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания и выполнения данными.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Apply(OperationApplyRequest request);
    }
}
