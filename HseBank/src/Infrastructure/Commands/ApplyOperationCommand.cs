using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    public class ApplyOperationCommand : ICommand
    {
        private OperationApplyRequest _request;
        private IOperationFactory _operationFactory;
        private IOperationRepository _operationRepository;
        private IBankAccountRepository _bankAccountRepository;
        private Operation? _operation;

        public ApplyOperationCommand(OperationApplyRequest request, IOperationFactory operationFactory, 
            IOperationRepository operationRepository, IBankAccountRepository bankAccountRepository)
        {
            _request = request;
            _operationFactory = operationFactory;
            _operationRepository = operationRepository;
            _bankAccountRepository = bankAccountRepository;
        }

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
