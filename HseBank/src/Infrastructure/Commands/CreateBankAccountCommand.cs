using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private BankAccountCreateRequest _request;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        public CreateBankAccountCommand(BankAccountCreateRequest request, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _request = request;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        public void Execute()
        {
            var account = _bankAccountFactory.Create(_request);
            _bankAccountRepository.Add(account);
        }

        public void Undo()
        {
            var account = _bankAccountRepository.GetByName(_request.name);
            if (account is not null)
            {
                _bankAccountRepository.Delete(account);
            }
        }
    }
}
