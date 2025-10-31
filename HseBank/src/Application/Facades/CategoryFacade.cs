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
    public class CategoryFacade : ICategoryFacade
    {
        private ILogger<BankAccountFacade> _logger;
        private ICommandFactory _commandFactory;
        private ICommandManager _commandManager;

        public CategoryFacade(ILogger<BankAccountFacade> logger, ICommandFactory commandFactory, ICommandManager commandManager)
        {
            _logger = logger;
            _commandFactory = commandFactory;
            _commandManager = commandManager;
        }

        public OperationResult Create(CategoryCreateRequest request)
        {
            ICommand command = _commandFactory.CreateCategoryCommand(request);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Категория создана успешно.");
            }
            catch (ValidationException ex)
            {
                _logger.LogWarning(ex, $"Ошибка валидации при создании категории: {ex.Message}");
                return OperationResult.Failure($"Ошибка валидации: {ex.Message}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при создании категории: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult ChangeName(string oldName, string newName)
        {
            ICommand command = _commandFactory.ChangeCategoryNameCommand(oldName, newName);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Название категории изменено успешно.");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при изменении названия категории: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }

        public OperationResult Delete(string name)
        {
            ICommand command = _commandFactory.DeleteCategoryCommand(name);
            try
            {
                _commandManager.Execute(command);
                return OperationResult.Success("Категория удалена успешно.");
            }
            catch (EntityNotFoundException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                return OperationResult.Failure(ex.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Критическая ошибка при удалении категории: {ex.Message}");
                return OperationResult.Failure($"Системная ошибка: {ex.Message}");
            }
        }
    }
}
