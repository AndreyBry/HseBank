using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Infrastructure.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Infrastructure.Factories
{
    /// <summary>
    /// Фабрика команд.
    /// </summary>
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="serviceProvider">Service Provider.</param>
        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Создание команды создания и выполнения операции по счету.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания и выполнения операции данными.</param>
        /// <returns>Команда.</returns>
        public ICommand ApplyOperationCommand(OperationApplyRequest request)
        {
            return new ApplyOperationCommand(
                request,
                _serviceProvider.GetRequiredService<IOperationFactory>(),
                _serviceProvider.GetRequiredService<IOperationRepository>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        /// <summary>
        /// Создание команды создания банковского счета.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания банковского счета данными.</param>
        /// <returns>Команда.</returns>
        public ICommand CreateBankAccountCommand(BankAccountCreateRequest request)
        {
            return new CreateBankAccountCommand(
                request,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        /// <summary>
        /// Создание команды для создания категории.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания категории данными.</param>
        /// <returns>Команда.</returns>
        public ICommand CreateCategoryCommand(CategoryCreateRequest request)
        {
            return new CreateCategoryCommand(
                request,
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        /// <summary>
        /// Создание команды получения всех банковских счетов.
        /// </summary>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<BankAccount>> GetAllBankAccountsCommand()
        {
            return new GetAllBankAccountsCommand(
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        /// <summary>
        /// Создание команды получение всех категорий.
        /// </summary>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<Category>> GetAllCategoriesCommand()
        {
            return new GetAllCategoriesCommand(
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        /// <summary>
        /// Создание команды получения категорий определенного типа.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<Category>> GetCategoriesByTypeCommand(TransactionType type)
        {
            return new GetCategoriesByTypeCommand(
                type,
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        /// <summary>
        /// Создание команды изменения названия банковского счета.
        /// </summary>
        /// <param name="oldName">Название счета, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Команда.</returns>
        public ICommand ChangeBankAccountNameCommand(string oldName, string newName)
        {
            return new ChangeBankAccountNameCommand(
                oldName,
                newName,
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        /// <summary>
        /// Создание команды изменения названия категории.
        /// </summary>
        /// <param name="oldName">Название категории, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Команда.</returns>
        public ICommand ChangeCategoryNameCommand(string oldName, string newName)
        {
            return new ChangeCategoryNameCommand(
                oldName,
                newName,
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        /// <summary>
        /// Создание команды удаления банковского счета.
        /// </summary>
        /// <param name="id">Айди счета.</param>
        /// <returns>Команда.</returns>
        public ICommand DeleteBankAccountCommand(Guid id)
        {
            return new DeleteBankAccountCommand(
                id,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        /// <summary>
        /// Создание команды удаления категории.
        /// </summary>
        /// <param name="id">Айди категории.</param>
        /// <returns>Команда.</returns>
        public ICommand DeleteCategoryCommand(Guid id)
        {
            return new DeleteCategoryCommand(
                id,
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        /// <summary>
        /// Создание декоратора для измерения времени выполнения команды.
        /// </summary>
        /// <param name="decoratedCommand">Команда, время выполнения которой необходимо измерить.</param>
        /// <returns>Команда.</returns>
        public ICommand TimedCommandDecorator(ICommand decoratedCommand)
        {
            return new TimedCommandDecorator(
                decoratedCommand,
                _serviceProvider.GetRequiredService<IMetricsService>()
                );
        }

        /// <summary>
        /// Создание команды для экспорта данных в файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Команда.</returns>
        public ICommand ExportDataCommand(string filePath, FileFormat fileFormat)
        {
            return new ExportDataCommand(
                filePath,
                fileFormat,
                _serviceProvider.GetRequiredService<IBankAccountRepository>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>(),
                _serviceProvider.GetRequiredService<IOperationRepository>()
                );
        }

        /// <summary>
        /// Создание команды для импорта данных из файла.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Команда.</returns>
        public ICommand ImportDataCommand(string filePath, FileFormat fileFormat)
        {
            return new ImportDataCommand(
                filePath,
                fileFormat,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<IOperationFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>(),
                _serviceProvider.GetRequiredService<IOperationRepository>()
                );
        }
    }
}
