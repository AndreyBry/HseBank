using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для создания банковского счета.
    /// </summary>
    public class CreateBankAccountCommand : ICommand
    {
        private BankAccountCreateRequest _request;
        private IBankAccountFactory _bankAccountFactory;
        private IBankAccountRepository _bankAccountRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания счета данными.</param>
        /// <param name="bankAccountFactory">Фабрика банковских счетов.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        public CreateBankAccountCommand(BankAccountCreateRequest request, IBankAccountFactory bankAccountFactory, IBankAccountRepository bankAccountRepository)
        {
            _request = request;
            _bankAccountFactory = bankAccountFactory;
            _bankAccountRepository = bankAccountRepository;
        }

        /// <summary>
        /// Создание счета и сохранение в репозитории.
        /// </summary>
        public void Execute()
        {
            var account = _bankAccountFactory.Create(_request);
            _bankAccountRepository.Add(account);
        }

        /// <summary>
        /// Удаление созданного счета.
        /// </summary>
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
