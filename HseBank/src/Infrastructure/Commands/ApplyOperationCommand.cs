using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для создания и выполнения операции по счету.
    /// </summary>
    public class ApplyOperationCommand : ICommand
    {
        private OperationApplyRequest _request;
        private IOperationFactory _operationFactory;
        private IOperationRepository _operationRepository;
        private IBankAccountRepository _bankAccountRepository;
        private Operation? _operation;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания и выполнения операции данными.</param>
        /// <param name="operationFactory">Фабрика операций.</param>
        /// <param name="operationRepository">Репозиторий операций.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        public ApplyOperationCommand(OperationApplyRequest request, IOperationFactory operationFactory, 
            IOperationRepository operationRepository, IBankAccountRepository bankAccountRepository)
        {
            _request = request;
            _operationFactory = operationFactory;
            _operationRepository = operationRepository;
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Создание, сохранение в репозитории и выполнение операции по счету.
        /// </summary>
        public void Execute()
        {
            _operation = _operationFactory.Create(_request);
            _operationRepository.Add(_operation);

            decimal amount = _request.type == TransactionType.Income ? _request.amount : -_request.amount;
            var account = _bankAccountRepository.GetById(_request.bankAccountId);
            if (account is not null)
            {
                account.UpdateBalance(amount);
                _bankAccountRepository.Update(account);
            }
        }

        /// <summary>
        /// Удаление операции и выполнение обратной операции по счету.
        /// </summary>
        public void Undo()
        {
            if (_operation is not null)
            {
                _operationRepository.Delete(_operation);

                decimal amount = _request.type == TransactionType.Income ? -_request.amount : _request.amount;
                var account = _bankAccountRepository.GetById(_request.bankAccountId);
                if (account is not null)
                {
                    account.UpdateBalance(amount);
                    _bankAccountRepository.Update(account);
                }
            }
        }
    }
}
