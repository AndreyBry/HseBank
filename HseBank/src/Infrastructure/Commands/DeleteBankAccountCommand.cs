using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Exceptions;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для удаления банковского счета.
    /// </summary>
    public class DeleteBankAccountCommand : ICommand
    {
        private Guid _id;
        private string _name = string.Empty;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Айди счета.</param>
        /// <param name="bankAccountFactory">Фабрика банковских счетов.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        public DeleteBankAccountCommand(Guid id, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _id = id;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Удаление счета.
        /// </summary>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если счет не существует.</exception>
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

        /// <summary>
        /// Создание удаленного счета.
        /// </summary>
        public void Undo()
        {
            var account = _bankAccountRepository.GetById(_id);
            if (account is null && _name != string.Empty)
            {
                var request = new BankAccountCreateRequest(_name, id: _id);
                account = _bankAccountFactory.Create(request);
                _bankAccountRepository.Add(account);
            }
        }
    }
}
