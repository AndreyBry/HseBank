using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    public class ChangeBankAccountNameCommand : ICommand
    {
        string _oldName;
        string _newName;
        private IBankAccountRepository _bankAccountRepository;

        public ChangeBankAccountNameCommand(string oldName, string newName, IBankAccountRepository bankAccountRepository)
        {
            _oldName = oldName;
            _newName = newName;
            _bankAccountRepository = bankAccountRepository;
        }

        public void Execute()
        {
            var account = _bankAccountRepository.GetByName(_oldName);
            if (account is null)
            {
                throw new EntityNotFoundException("счет", _oldName);
            }
            account.UpdateName(_newName);
            _bankAccountRepository.Update(account);
        }

        public void Undo()
        {
            var account = _bankAccountRepository.GetByName(_newName);
            if (account is not null)
            {
                account.UpdateName(_oldName);
                _bankAccountRepository.Update(account);
            }
        }
    }
}
