using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Validation
{
    /// <summary>
    /// Валидатор операций.
    /// </summary>
    public class OperationValidator : IOperationValidator
    {
        private IBankAccountRepository _bankAccountRepository;
        private ICategoryRepository _categoryRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        public OperationValidator(IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository)
        {
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Валидация операции.
        /// </summary>
        /// <param name="request">ДТО с данными об операции.</param>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если операция ссылается на несуществующий счет или категорию.</exception>
        /// <exception cref="BusinessRuleException">Выбрасывается, если на счете недостаточно средств.</exception>
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
