using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Factories
{
    public class OperationFactory : IOperationFactory
    {
        private IOperationValidator _operationValidator;

        public OperationFactory(IOperationValidator operationValidator)
        {
            _operationValidator = operationValidator;
        }

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
            _operationValidator.Validate(request);
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.UtcNow;
            return new Operation(id, request.type, request.bankAccountId, request.amount, date, request.categoryId, request.description);
        }
    }
}
