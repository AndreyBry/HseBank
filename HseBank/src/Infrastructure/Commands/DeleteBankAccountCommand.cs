using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class DeleteBankAccountCommand : ICommand
    {
        private Guid _id;
        private string _name = string.Empty;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        public DeleteBankAccountCommand(Guid id, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _id = id;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        public void Execute()
        {
            var account = _bankAccountRepository.GetById(_id);
            if (account is null)
            {
                throw new EntityNotFoundException("счет", _id);
            }
            _name = account.Name;
            _bankAccountRepository.Delete(account);
        }

        public void Undo()
        {
            var account = _bankAccountRepository.GetById(_id);
            if (account is null && _name != string.Empty)
            {
                var request = new BankAccountCreateRequest(_name);
                account = _bankAccountFactory.Create(request);
                _bankAccountRepository.Add(account);
            }
        }
    }
}
