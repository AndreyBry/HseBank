using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;
using Microsoft.Extensions.Logging;

namespace HseBank.src.Application.Facades
{
    public class BankAccountFacade : IBankAccountFacade
    {
        private ILogger<BankAccountFacade> _logger;
        private ICommandFactory _commandFactory;
        private ICommandManager _commandManager;

        public BankAccountFacade(ILogger<BankAccountFacade> logger, ICommandFactory commandFactory, ICommandManager commandManager)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _commandManager = commandManager;
        }

        public OperationResult Create(BankAccountCreateRequest request)
        {
            ICommand command = _commandFactory.CreateBankAccountCommand(request);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Счет создан успешно.");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, $"Ошибка валидации при создании счета: {ex.Message}");
                return OperationResult.Failure($"Ошибка валидации: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при создании счета: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult<IEnumerable<BankAccount>> GetAll()
        {
            var command = _commandFactory.GetAllBankAccountsCommand();
            try
            {
                IEnumerable<BankAccount> accounts = _commandManager.Execute(command);
                return OperationResult<IEnumerable<BankAccount>>.Success(accounts, "Информация о счетах получена успешно.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при получении информации о счетах: {ex.Message}");
                return OperationResult<IEnumerable<BankAccount>>.Failure(new List<BankAccount>(), $"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult ChangeName(string oldName, string newName)
        {
            ICommand command = _commandFactory.ChangeBankAccountNameCommand(oldName, newName);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Название счета изменено успешно.");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при изменении названия счета: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult Delete(Guid id)
        {
            ICommand command = _commandFactory.DeleteBankAccountCommand(id);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Счет удален успешно.");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при удалении счета: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }
    }
}
