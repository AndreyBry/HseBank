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
    /// <summary>
    /// Фасад для работы с операциями.
    /// </summary>
    public class OperationFacade : IOperationFacade
    {
        private ILogger<OperationFacade> _logger;
        private ICommandFactory _commandFactory;
        private ICommandManager _commandManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="commandFactory">Фабрика команд.</param>
        /// <param name="commandManager">Менеджер команд.</param>
        public OperationFacade(ILogger<OperationFacade> logger, ICommandFactory commandFactory, ICommandManager commandManager)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _commandManager = commandManager;
        }

        /// <summary>
        /// Метод для создания и выполения операции.
        /// </summary>
        /// <param name="request">ДТО с данными для создания и выполнения.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Apply(OperationApplyRequest request)
        {
            ICommand command = _commandFactory.ApplyOperationCommand(request);
            ICommand timedCommand = _commandFactory.TimedCommandDecorator(command);
            try
            {
                _commandManager.Execute(timedCommand);
                return OperationResult.Success("Операция выполнена успешно.");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, $"Ошибка валидации при выполнении операции: {ex.Message}");
                return OperationResult.Failure($"Ошибка валидации: {ex.Message}");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (BusinessRuleException ex)
            {
                _logger.LogWarning(ex, $"Логическая ошибка при выполнении операции: {ex.Message}");
                return OperationResult.Failure($"Логическая ошибка: {ex.Message}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при выполнении операции: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }
    }
}
