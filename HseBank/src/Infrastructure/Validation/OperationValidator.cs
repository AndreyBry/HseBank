using HseBank.src.Domain.DTOs;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;

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

        public void Validate(OperationCreateRequest request)
        {
            if (_bankAccountRepository.GetById(request.bankAccountId) is null)
            {
                throw new ArgumentException("Банковский счет не найден.");
            }
            if (_categoryRepository.GetById(request.categoryId) is null)
            {
                throw new ArgumentException("Категория не найдена.");
            }
        }
    }
}
