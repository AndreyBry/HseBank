using HseBank.src.Domain.DTOs;
using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;

namespace HseBank.src.Infrastructure.Factories
{
    public class OperationFactory : IOperationFactory
    {
        private IOperationValidator _operationValidator;

        public OperationFactory(IOperationValidator operationValidator)
        {
            _operationValidator = operationValidator;
        }

        public Operation Create(OperationCreateRequest request)
        {
            if (request.amount <= 0)
            {
                throw new ArgumentException("Сумма операции должна быть положительным числом.");
            }
            if (request.description?.Length > 50)
            {
                throw new ArgumentException("Описание операции может содержать максимум 50 символов.");
            }
            _operationValidator.Validate(request);
            Guid id = Guid.NewGuid();
            DateTime date = DateTime.UtcNow;
            return new Operation(id, request.type, request.bankAccountId, request.amount, date, request.categoryId, request.description);
        }
    }
}
