using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    /// <summary>
    /// Фабрика операций.
    /// </summary>
    public class OperationFactory : IOperationFactory
    {
        private IOperationValidator _operationValidator;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="operationValidator">Валидатор операций.</param>
        public OperationFactory(IOperationValidator operationValidator)
        {
            _operationValidator = operationValidator;
        }

        /// <summary>
        /// Создание корректной операции.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания операции данными.</param>
        /// <returns>Операция.</returns>
        /// <exception cref="ValidationException">Выбрасывается, если валидация не пройдена.</exception>
        public Operation Create(OperationApplyRequest request)
        {
            if (request.amount <= 0)
            {
                throw new ValidationException("Сумма операции должна быть положительным числом.");
            }
            if (request.description?.Length > 50)
            {
                throw new ValidationException("Описание операции может содержать максимум 50 символов.");
            }
            if (request.needValidateBalance)
            {
                _operationValidator.Validate(request);
            }
            Guid id = request.id ?? Guid.NewGuid();
            DateTime date = request.date ?? DateTime.UtcNow;
            return new Operation(id, request.type, request.bankAccountId, request.amount, date, request.categoryId, request.description);
        }
    }
}
