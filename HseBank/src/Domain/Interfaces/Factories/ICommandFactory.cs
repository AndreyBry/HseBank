using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    /// <summary>
    /// Предоставляет методы для корректного создания команд.
    /// </summary>
    public interface ICommandFactory
    {
        /// <summary>
        /// Создание команды создания банковского счета.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания банковского счета данными.</param>
        /// <returns>Команда.</returns>
        public ICommand CreateBankAccountCommand(BankAccountCreateRequest request);
        /// <summary>
        /// Создание команды для создания категории.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания категории данными.</param>
        /// <returns>Команда.</returns>
        public ICommand CreateCategoryCommand(CategoryCreateRequest request);
        /// <summary>
        /// Создание команды создания и выполнения операции по счету.
        /// </summary>
        /// <param name="request">ДТО с необходимыми для создания и выполнения операции данными.</param>
        /// <returns>Команда.</returns>
        public ICommand ApplyOperationCommand(OperationApplyRequest request);

        /// <summary>
        /// Создание команды получения всех банковских счетов.
        /// </summary>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<BankAccount>> GetAllBankAccountsCommand();
        /// <summary>
        /// Создание команды получение всех категорий.
        /// </summary>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<Category>> GetAllCategoriesCommand();
        /// <summary>
        /// Создание команды получения категорий определенного типа.
        /// </summary>
        /// <param name="type">Тип категории.</param>
        /// <returns>Команда.</returns>
        public IQueryCommand<IEnumerable<Category>> GetCategoriesByTypeCommand(TransactionType type);

        /// <summary>
        /// Создание команды изменения названия банковского счета.
        /// </summary>
        /// <param name="oldName">Название счета, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Команда.</returns>
        public ICommand ChangeBankAccountNameCommand(string oldName, string newName);
        /// <summary>
        /// Создание команды изменения названия категории.
        /// </summary>
        /// <param name="oldName">Название категории, которое необходимо изменить.</param>
        /// <param name="newName">Новое название.</param>
        /// <returns>Команда.</returns>
        public ICommand ChangeCategoryNameCommand(string oldName, string newName);

        /// <summary>
        /// Создание команды удаления банковского счета.
        /// </summary>
        /// <param name="id">Айди счета.</param>
        /// <returns>Команда.</returns>
        public ICommand DeleteBankAccountCommand(Guid id);
        /// <summary>
        /// Создание команды удаления категории.
        /// </summary>
        /// <param name="id">Айди категории.</param>
        /// <returns>Команда.</returns>
        public ICommand DeleteCategoryCommand(Guid id);

        /// <summary>
        /// Создание декоратора для измерения времени выполнения команды.
        /// </summary>
        /// <param name="decoratedCommand">Команда, время выполнения которой необходимо измерить.</param>
        /// <returns>Команда.</returns>
        public ICommand TimedCommandDecorator(ICommand decoratedCommand);

        /// <summary>
        /// Создание команды для экспорта данных в файл.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Команда.</returns>
        public ICommand ExportDataCommand(string filePath, FileFormat fileFormat);
        /// <summary>
        /// Создание команды для импорта данных из файла.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <returns>Команда.</returns>
        public ICommand ImportDataCommand(string filePath, FileFormat fileFormat);
    }
}
