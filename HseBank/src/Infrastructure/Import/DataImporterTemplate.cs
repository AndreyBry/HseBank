using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Exceptions;

namespace HseBank.src.Infrastructure.Import
{
    /// <summary>
    /// Выполняет импорт данных из файла.
    /// </summary>
    public abstract class DataImporterTemplate
    {
        protected readonly List<BankAccount> _importedBankAccounts = new();
        protected readonly List<Category> _importedCategories = new();
        protected readonly List<Operation> _importedOperations = new();

        public IEnumerable<BankAccount> ImportedBankAccounts => _importedBankAccounts;
        public IEnumerable<Category> ImportedCategories => _importedCategories;
        public IEnumerable<Operation> ImportedOperations => _importedOperations;

        /// <summary>
        /// Импорт данных из файла.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <exception cref="ValidationException">Выбрасывается, если файл с таким именем не существует.</exception>
        public void Import(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new ValidationException("Файл с таким именем не найден.");
            }
            var fileContent = ReadFile(filePath);
            ParseData(fileContent);
            ValidateData();
        }

        /// <summary>
        /// Очистка хранилища десериализованных объектов.
        /// </summary>
        public void ClearImportedList()
        {
            _importedBankAccounts.Clear();
            _importedCategories.Clear();
            _importedOperations.Clear();
        }

        /// <summary>
        /// Получение содержимого файла.
        /// </summary>
        /// <param name="filePath">Путь до файла.</param>
        /// <returns>Содержимое файла.</returns>
        protected virtual string ReadFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        /// <summary>
        /// Валидация десериализованных объектов.
        /// </summary>
        /// <exception cref="ValidationException">Возникает, если валидация не пройдена.</exception>
        protected virtual void ValidateData()
        {
            var errors = new List<string>();

            var accountIds = _importedBankAccounts.Select(a => a.Id).ToHashSet();
            var categoryIds = _importedCategories.Select(c => c.Id).ToHashSet();
            var operationIds = _importedOperations.Select(o => o.Id).ToHashSet();

            if (_importedBankAccounts.Count != accountIds.Count)
            {
                throw new ValidationException("Обнаружены дублирующиеся ID счетов.");
            }
            if (_importedCategories.Count != categoryIds.Count)
            {
                throw new ValidationException("Обнаружены дублирующиеся ID категорий.");
            }
            if (_importedOperations.Count != operationIds.Count)
            {
                throw new ValidationException("Обнаружены дублирующиеся ID операций.");
            }
        }

        /// <summary>
        /// Парсинг объектов.
        /// </summary>
        /// <param name="fileContent">Содержимое файла.</param>
        protected abstract void ParseData(string fileContent);
    }
}
