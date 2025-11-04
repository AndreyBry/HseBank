using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Repositories;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для изменения названия банковского счета.
    /// </summary>
    public class ChangeBankAccountNameCommand : ICommand
    {
        string _oldName;
        string _newName;
        private IBankAccountRepository _bankAccountRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="oldName">Название счета, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        public ChangeBankAccountNameCommand(string oldName, string newName, IBankAccountRepository bankAccountRepository)
        {
            _oldName = oldName;
            _newName = newName;
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Изменение названия счета.
        /// </summary>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если счет не существует.</exception>
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

        /// <summary>
        /// Возврат старого названия счета.
        /// </summary>
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
