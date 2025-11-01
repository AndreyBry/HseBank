using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Results;
using Microsoft.Extensions.Logging;

namespace HseBank.src.Application.Facades
{
    public class UndoRedoFacade : IUndoRedoFacade
    {
        private ILogger<UndoRedoFacade> _logger;
        private ICommandManager _commandManager;

        public UndoRedoFacade(ILogger<UndoRedoFacade> logger, ICommandManager commandManager)
        {
            _logger = logger;
            _commandManager = commandManager;
        }

        public OperationResult Undo()
        {
            if(!_commandManager.CanUndo)
            {
                return OperationResult.Failure("Нет действий для отмены.");
            }
            try
            {
                _commandManager.Undo();
                return OperationResult.Success("Действие отменено успешно.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при отмене действия: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult Redo()
        {
            if(!_commandManager.CanRedo)
            {
                return OperationResult.Failure("Нет действий для повторного выполнения.");
            }
            try
            {
                _commandManager.Redo();
                return OperationResult.Success("Действие выполнено повторно успешно.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при повторном выполнении действия: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }
    }
}
