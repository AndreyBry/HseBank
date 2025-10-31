using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class DeleteBankAccountCommand : ICommand
    {
        private string _name;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        public DeleteBankAccountCommand(string name, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _name = name;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        public void Execute()
        {
            var account = _bankAccountRepository.GetByName(_name);
            if (account is null)
            {
                throw new EntityNotFoundException("счет", _name);
            }
            _bankAccountRepository.Delete(account);
        }

        public void Undo()
        {
            var account = _bankAccountRepository.GetByName(_name);
            if (account is null)
            {
                var request = new BankAccountCreateRequest(_name);
                account = _bankAccountFactory.Create(request);
                _bankAccountRepository.Add(account);
            }
        }
    }
}
