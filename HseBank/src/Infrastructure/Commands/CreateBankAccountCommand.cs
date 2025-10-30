using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class CreateBankAccountCommand : ICommand
    {
        private string _bankAccountName;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        public CreateBankAccountCommand(string bankAccountName, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _bankAccountName = bankAccountName;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        public void Execute()
        {
            BankAccount account = _bankAccountFactory.Create(_bankAccountName);
            _bankAccountRepository.Add(account);
        }
    }
}
