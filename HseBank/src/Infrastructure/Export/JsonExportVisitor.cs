using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Export;
using System.Text.Json;

namespace HseBank.src.Infrastructure.Export
{
    /// <summary>
    /// Выполняет экспорт данных об объектах в JSON-файл.
    /// </summary>
    public class JsonExportVisitor : IDataVisitor
    {
        private readonly List<object> _data = new();
        private readonly JsonSerializerOptions _options;

        /// <summary>
        /// Конструктор. Формирует параметры сериализации.
        /// </summary>
        public JsonExportVisitor()
        {
            _options = new JsonSerializerOptions
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };
        }

        /// <summary>
        /// Сериализует данные о банковском счете.
        /// </summary>
        /// <param name="bankAccount">Банковский счет.</param>
        public void Visit(BankAccount bankAccount)
        {
            _data.Add(new
            {
                Type = "BankAccount",
                Id = bankAccount.Id,
                Name = bankAccount.Name,
                Balance = bankAccount.Balance
            });
        }

        /// <summary>
        /// Сериализует данные о категории.
        /// </summary>
        /// <param name="category"></param>
        public void Visit(Category category)
        {
            _data.Add(new
            {
                Type = "Category",
                Id = category.Id,
                Name = category.Name,
                CategoryType = category.Type.ToString()
            });
        }

        /// <summary>
        /// Сериализует данные об операции.
        /// </summary>
        /// <param name="operation"></param>
        public void Visit(Operation operation)
        {
            _data.Add(new
            {
                Type = "Operation",
                Id = operation.Id,
                CategoryType = operation.Type.ToString(),
                Amount = operation.Amount,
                BankAccountId = operation.BankAccountId,
                CategoryId = operation.CategoryId,
                Date = operation.Date,
                Description = operation.Description
            });
        }

        /// <summary>
        /// Получение результата сериализации всех объектов.
        /// </summary>
        /// <returns>Все сериализованные объекты.</returns>
        public string GetResult()
        {
            return JsonSerializer.Serialize(_data, _options);
        }

        /// <summary>
        /// Сброс накопленного результата сериализации.
        /// </summary>
        public void Reset()
        {
            _data.Clear();
        }
    }
}
