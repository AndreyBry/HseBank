using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.Results;
using Microsoft.Extensions.Logging;

namespace HseBank.src.Application.Facades
{
    /// <summary>
    /// Фасад для экспорта данных в файл.
    /// </summary>
    public class ExportFacade : IExportFacade
    {
        private ILogger<ExportFacade> _logger;
        private ICommandFactory _commandFactory;
        private ICommandManager _commandManager;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="logger">Логгер.</param>
        /// <param name="commandFactory">Фабрика команд.</param>
        /// <param name="commandManager">Менеджер команд.</param>
        public ExportFacade(ILogger<ExportFacade> logger, ICommandFactory commandFactory, ICommandManager commandManager)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _commandManager = commandManager;
        }

        /// <summary>
        /// Метод для экспорта данных в файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Результат, содержащий статус выполнения и сообщение.</returns>
        public OperationResult Export(string filePath, FileFormat fileFormat)
        {
            ICommand command = _commandFactory.ExportDataCommand(filePath, fileFormat);
            ICommand timedCommand = _commandFactory.TimedCommandDecorator(command);
            try
            {
                _commandManager.Execute(timedCommand);
                return OperationResult.Success("Данные экспортированы успешно.");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при эскпорте данных: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }
    }
}
