using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Results;
using Microsoft.Extensions.Logging;

namespace HseBank.src.Application.Facades
{
    public class UndoFacade : IUndoFacade
    {
        private ILogger<UndoFacade> _logger;
        private ICommandManager _commandManager;

        public UndoFacade(ILogger<UndoFacade> logger, ICommandManager commandManager)
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
    }
}
