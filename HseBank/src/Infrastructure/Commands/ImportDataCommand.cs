using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Infrastructure.Import;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для импорта данных из файла.
    /// </summary>
    public class ImportDataCommand : ICommand
    {
        private string _filePath;
        private DataImporterTemplate _importerTemplate;
        private IBankAccountRepository _bankAccountRepository;
        private ICategoryRepository _categoryRepository;
        private IOperationRepository _operationRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <param name="bankAccountFactory">Фабрика банковских счетов.</param>
        /// <param name="categoryFactory">Фабрика категорий.</param>
        /// <param name="operationFactory">Фабрика операций.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        /// <param name="operationRepository">Репозиторий операций.</param>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такой формат файла не поддерживается.</exception>
        public ImportDataCommand(string filePath, FileFormat fileFormat, IBankAccountFactory bankAccountFactory, ICategoryFactory categoryFactory, IOperationFactory operationFactory,
            IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository, IOperationRepository operationRepository)
        {
            _filePath = filePath;
            _importerTemplate = fileFormat switch
            {
                FileFormat.Json => new JsonDataImporter(bankAccountFactory, categoryFactory, operationFactory, bankAccountRepository, categoryRepository, operationRepository),
                FileFormat.Csv => new CsvDataImporter(bankAccountFactory, categoryFactory, operationFactory, bankAccountRepository, categoryRepository, operationRepository),
                _ => throw new EntityNotFoundException("формат файла", fileFormat.ToString())
            };
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _operationRepository = operationRepository;
        }

        /// <summary>
        /// Импорт данных из файла.
        /// </summary>
        public void Execute()
        {
            _importerTemplate.Import(_filePath);
        }

        /// <summary>
        /// Удаление импортированных данных.
        /// </summary>
        public void Undo()
        {
            foreach (var account in _importerTemplate.ImportedBankAccounts)
            {
                _bankAccountRepository.Delete(account);
            }

            foreach (var category in _importerTemplate.ImportedCategories)
            {
                _categoryRepository.Delete(category);
            }

            foreach (var operation in _importerTemplate.ImportedOperations)
            {
                _operationRepository.Delete(operation);
            }

            _importerTemplate.ClearImportedList();
        }
    }
}
