using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;
using System.Text;

namespace HseBank.src.Infrastructure.Import
{
    /// <summary>
    /// Выполняет импорт данных из CSV-файла.
    /// </summary>
    public class CsvDataImporter : DataImporterTemplate
    {
        private IBankAccountFactory _bankAccountFactory;
        private ICategoryFactory _categoryFactory;
        private IOperationFactory _operationFactory;
        private IBankAccountRepository _bankAccountRepository;
        private ICategoryRepository _categoryRepository;
        private IOperationRepository _operationRepository;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="bankAccountFactory">Фабрика банковских счетов.</param>
        /// <param name="categoryFactory">Фабрика категорий.</param>
        /// <param name="operationFactory">Фабрика операций.</param>
        /// <param name="bankAccountRepository">Репозиторий банковских счетов.</param>
        /// <param name="categoryRepository">Репозиторий категорий.</param>
        /// <param name="operationRepository">Репозиторий операций.</param>
        public CsvDataImporter(IBankAccountFactory bankAccountFactory, ICategoryFactory categoryFactory, IOperationFactory operationFactory, 
            IBankAccountRepository bankAccountRepository, ICategoryRepository categoryRepository, IOperationRepository operationRepository)
        {
            _bankAccountFactory = bankAccountFactory;
            _categoryFactory = categoryFactory;
            _operationFactory = operationFactory;
            _bankAccountRepository = bankAccountRepository;
            _categoryRepository = categoryRepository;
            _operationRepository = operationRepository;
        }

        /// <summary>
        /// Парсинг содержимого файла.
        /// </summary>
        /// <param name="fileContent">Содержимое файла.</param>
        /// <exception cref="ValidationException">Выбрасывается, если в файле нет данных.</exception>
        protected override void ParseData(string fileContent)
        {
            var lines = fileContent.Split('\n').Where(line => !string.IsNullOrWhiteSpace(line)).ToArray();
            if (lines.Length < 2)
            {
                throw new ValidationException("CSV файл пуст или содержит только заголовки");
            }
            for (int i = 1; i < lines.Length; i++)
            {
                ParseCsvLine(lines[i]);
            }
        }

        /// <summary>
        /// Парсинг строки.
        /// </summary>
        /// <param name="line">Строка.</param>
        /// <exception cref="ValidationException">Выбрасывается, если недостаточно параметров в строке.</exception>
        private void ParseCsvLine(string line)
        {
            var values = ParseCsvValues(line);
            if (values.Length < 2)
            {
                throw new ValidationException("Не достаточно информации об объекте.");
            }
            var type = values[0];
            var id = Guid.TryParse(values[1], out var guid) ? guid : Guid.NewGuid();
            switch (type.ToLower())
            {
                case "bankaccount":
                    ParseCsvBankAccount(values, id);
                    break;
                case "category":
                    ParseCsvCategory(values, id);
                    break;
                case "operation":
                    ParseCsvOperation(values, id);
                    break;
            }
        }

        /// <summary>
        /// Парсинг банковского счета.
        /// </summary>
        /// <param name="values">Значения.</param>
        /// <param name="id">Айди.</param>
        /// <exception cref="ValidationException">Выбрасывается, если недостаточно значения для импорта банковского счета.</exception>
        private void ParseCsvBankAccount(string[] values, Guid id)
        {
            if (values.Length < 4)
            {
                throw new ValidationException("Не достаточно информации о счете.");
            }
            var name = UnescapeCsv(values[2]);
            var balance = decimal.TryParse(values[3], out var bal) ? bal : 0;
            var account = _bankAccountFactory.Create(new BankAccountCreateRequest(name, balance, id: id));
            _importedBankAccounts.Add(account);
            _bankAccountRepository.Add(account);
        }

        /// <summary>
        /// Парсинг категории.
        /// </summary>
        /// <param name="values">Значения.</param>
        /// <param name="id">Айди.</param>
        /// <exception cref="ValidationException">Выбрасывается, если недостаточно значений для импорта категории.</exception>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такого типа категории не существует.</exception>
        private void ParseCsvCategory(string[] values, Guid id)
        {
            if (values.Length < 5)
            {
                throw new ValidationException("Не достаточно информации о категории.");
            }
            var name = UnescapeCsv(values[2]);
            var typeStr = values[4];
            if (Enum.TryParse<TransactionType>(typeStr, out var type))
            {
                var category = _categoryFactory.Create(new CategoryCreateRequest(type, name, id: id));
                _importedCategories.Add(category);
                _categoryRepository.Add(category);
            }
            else
            {
                throw new EntityNotFoundException("тип категории", typeStr);
            }
        }

        /// <summary>
        /// Парсинг операции.
        /// </summary>
        /// <param name="values">Значения.</param>
        /// <param name="id">Айди.</param>
        /// <exception cref="ValidationException">Выбрасывается, если недостаточно значений для импорта операции.</exception>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такой тип категории не существует.</exception>
        private void ParseCsvOperation(string[] values, Guid id)
        {
            if (values.Length < 10)
            {
                throw new ValidationException("Не достаточно информации об операции.");
            }
            var amount = decimal.TryParse(values[5], out var amt) ? amt : 100m;
            var typeStr = values[4];
            var accountId = Guid.TryParse(values[6], out var accId) ? accId : Guid.Empty;
            var categoryId = Guid.TryParse(values[7], out var catId) ? catId : Guid.Empty;
            var date = DateTime.TryParse(values[8], out var opDate) ? opDate : DateTime.Now;
            var description = UnescapeCsv(values[9]);
            if (Enum.TryParse<TransactionType>(typeStr, out var type))
            {
                var request = new OperationApplyRequest(type, accountId, amount, categoryId, description, date: date, id: id, needValidateBalance: false);
                var operation = _operationFactory.Create(request);
                _importedOperations.Add(operation);
                _operationRepository.Add(operation);
            }
            else
            {
                throw new EntityNotFoundException("тип категории", typeStr);
            }
        }

        /// <summary>
        /// Парсинг значений из сериализованной строки.
        /// </summary>
        /// <param name="line">Строка.</param>
        /// <returns>Значения.</returns>
        private string[] ParseCsvValues(string line)
        {
            var values = new List<string>();
            var inQuotes = false;
            var currentValue = new StringBuilder();
            foreach (var ch in line)
            {
                if (ch == '"')
                {
                    inQuotes = !inQuotes;
                }
                else if (ch == ',' && !inQuotes)
                {
                    values.Add(currentValue.ToString());
                    currentValue.Clear();
                }
                else
                {
                    currentValue.Append(ch);
                }
            }
            values.Add(currentValue.ToString());
            return values.ToArray();
        }

        /// <summary>
        /// Десериализация значения из CSV.
        /// </summary>
        /// <param name="value">Значение.</param>
        /// <returns>Десериализованное значение.</returns>
        private string UnescapeCsv(string value)
        {
            if (value.StartsWith('"') && value.EndsWith('"'))
            {
                value = value.Substring(1, value.Length - 2);
                value = value.Replace("\"\"", "\"");
            }
            return value.Trim();
        }
    }
}
