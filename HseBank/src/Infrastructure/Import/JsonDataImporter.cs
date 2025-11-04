using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Exceptions;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;
using System.Text.Json;

namespace HseBank.src.Infrastructure.Import
{
    /// <summary>
    /// Выполняет импорт данных из JSON-файла.
    /// </summary>
    public class JsonDataImporter : DataImporterTemplate
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
        public JsonDataImporter(IBankAccountFactory bankAccountFactory, ICategoryFactory categoryFactory, IOperationFactory operationFactory, 
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
        /// Парсинг данных из файла.
        /// </summary>
        /// <param name="fileContent">Содержимое файла.</param>
        /// <exception cref="ValidationException">Выбрасывается, если десериализация содержимого файла не может быть выполнена.</exception>
        protected override void ParseData(string fileContent)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
            var importedData = JsonSerializer.Deserialize<List<JsonImportItem>>(fileContent, options);
            if (importedData == null)
            {
                throw new ValidationException("Неверный формат JSON файла.");
            }
            foreach (var item in importedData)
            {
                switch (item.Type?.ToLower())
                {
                    case "bankaccount":
                        ParseBankAccount(item);
                        break;
                    case "category":
                        ParseCategory(item);
                        break;
                    case "operation":
                        ParseOperation(item);
                        break;
                }
            }

        }

        /// <summary>
        /// Парсинг банковского счета.
        /// </summary>
        /// <param name="item">Банковский счет.</param>
        private void ParseBankAccount(JsonImportItem item)
        {
            var account = _bankAccountFactory.Create(new BankAccountCreateRequest(item.Name, item.Balance ?? 0m, id: item.Id));
            _importedBankAccounts.Add(account);
            _bankAccountRepository.Add(account);
        }

        /// <summary>
        /// Парсинг категории.
        /// </summary>
        /// <param name="item">Категория.</param>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такой тип категории не поддерживается.</exception>
        private void ParseCategory(JsonImportItem item)
        {
            if (!Enum.TryParse<TransactionType>(item.CategoryType, out var type))
            {
                throw new EntityNotFoundException("тип категории", item.CategoryType ?? "");
            }
            var category = _categoryFactory.Create(new CategoryCreateRequest(type, item.Name, id: item.Id));
            _importedCategories.Add(category);
            _categoryRepository.Add(category);
        }

        /// <summary>
        /// Парсинг операции.
        /// </summary>
        /// <param name="item">Операция.</param>
        /// <exception cref="EntityNotFoundException">Выбрасывается, если такой тип операции не поддерживается.</exception>
        private void ParseOperation(JsonImportItem item)
        {
            if (!Enum.TryParse<TransactionType>(item.CategoryType, out var type))
            {
                throw new EntityNotFoundException("тип категории", item.CategoryType ?? "");
            }
            var request = new OperationApplyRequest(type, item.BankAccountId ?? Guid.Empty, item.Amount ?? 100m, item.CategoryId ?? Guid.Empty, item.Description, date: item.Date, id: item.Id, needValidateBalance: false);
            var operation = _operationFactory.Create(request);
            _importedOperations.Add(operation);
            _operationRepository.Add(operation);
        }

        /// <summary>
        /// Описывает значения, которые необходимо получить при помощи десериализации содержимого файла.
        /// </summary>
        private class JsonImportItem
        {
            public string Type { get; set; } = "";
            public Guid? Id { get; set; }
            public string Name { get; set; } = "";
            public decimal? Balance { get; set; }
            public string? CategoryType { get; set; }
            public decimal? Amount { get; set; }
            public Guid? BankAccountId { get; set; }
            public Guid? CategoryId { get; set; }
            public DateTime? Date { get; set; }
            public string? Description { get; set; }
        }
    }
}
