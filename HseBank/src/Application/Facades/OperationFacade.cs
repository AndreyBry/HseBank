using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;
using Microsoft.Extensions.Logging;

namespace HseBank.src.Application.Facades
{
    public class OperationFacade : IOperationFacade
    {
        private ILogger<OperationFacade> _logger;
        private ICommandFactory _commandFactory;
        private ICommandManager _commandManager;

        public OperationFacade(ILogger<OperationFacade> logger, ICommandFactory commandFactory, ICommandManager commandManager)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _commandManager = commandManager;
        }

        public OperationResult Apply(OperationApplyRequest request)
        {
            var command = _commandFactory.ApplyOperationCommand(request);
            try
            {
                _commandManager.Execute(command);
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
