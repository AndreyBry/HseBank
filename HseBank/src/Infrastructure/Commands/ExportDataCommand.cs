using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Export;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Infrastructure.Export;

namespace HseBank.src.Infrastructure.Commands
{
    /// <summary>
    /// Команда для экспорта данных в файл.
    /// </summary>
    public class ExportDataCommand : ICommand
    {
        private string _filePath;
        private FileFormat _fileFormat;
        private IBankAccountRepository _bankAccountRepository;
        private ICategoryRepository _categoryRepository;
        private IOperationRepository _operationRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <param name="fileFormat">Тип файла.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        /// <param name="operationRepository">Репозиторий операций.</param>
        public ExportDataCommand(string filePath, FileFormat fileFormat, IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository, IOperationRepository operationRepository)
        {
            _filePath = filePath;
            _fileFormat = fileFormat;
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _operationRepository = operationRepository;
        }

        /// <summary>
        /// Экспорт данных в файл.
        /// </summary>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такой формат файла не поддерживается.</exception>
        public void Execute()
        {
            IDataVisitor visitor = _fileFormat switch
            {
                FileFormat.Json => new JsonExportVisitor(),
                FileFormat.Csv => new CsvExportVisitor(),
                _ => throw new EntityNotFoundException("формат файла", _fileFormat.ToString())
            };
            ExportData(visitor);
            SaveToFile(visitor.GetResult());
        }

        /// <summary>
        /// Удаление файл, в который были экспортированы данные.
        /// </summary>
        public void Undo()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }
        }

        /// <summary>
        /// Выполнение экспорт пообъектно.
        /// </summary>
        /// <param name="visitor">Объект, который производит экспорт объектов.</param>
        private void ExportData(IDataVisitor visitor)
        {
            foreach (var account in _bankAccountRepository.GetAll())
            {
                account.Accept(visitor);
            }

            foreach (var category in _categoryRepository.GetAll())
            {
                category.Accept(visitor);
            }

            foreach (var operation in _operationRepository.GetAll())
            {
                operation.Accept(visitor);
            }
        }

        /// <summary>
        /// Сохранение файла с экспортированными данными.
        /// </summary>
        /// <param name="content">Данные.</param>
        private void SaveToFile(string content)
        {
            var directory = Path.GetDirectoryName(_filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(_filePath, content);
        }
    }
}
