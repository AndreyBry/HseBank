using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Validation
{
    public class OperationValidator : IOperationValidator
    {
        private IBankAccountRepository _bankAccountRepository;
        private ICategoryRepository _categoryRepository;

        public OperationValidator(IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
        }

        public void Validate(OperationApplyRequest request)
        {
            var account = _bankAccountRepository.GetById(request.bankAccountId);
            if (account is null)
            {
                throw new EntityNotFoundException("счет", request.bankAccountId);
            }
            var category = _categoryRepository.GetById(request.categoryId);
            if (category is null)
            {
                throw new EntityNotFoundException("категория", request.categoryId);
            }
            if (category.Type == TransactionType.Expense && account.Balance < request.amount)
            {
                throw new BusinessRuleException("Недостаточно средств на счете.");
            }
        }
    }
}
