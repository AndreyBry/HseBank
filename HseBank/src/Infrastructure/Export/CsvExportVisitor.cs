using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Interfaces.Export;
using System.Text;

namespace HseBank.src.Infrastructure.Export
{
    /// <summary>
    /// Выполняет экспорт данных об объектах в CSV-файл.
    /// </summary>
    public class CsvExportVisitor : IDataVisitor
    {
        private readonly StringBuilder _csvBuilder = new();
        private bool _headersWritten = false;

        /// <summary>
        /// Конструктор. Заполняет заголовки.
        /// </summary>
        public CsvExportVisitor()
        {
            EnsureHeadersWritten();
        }

        /// <summary>
        /// Сериализует данные о банковском счете для экспорта в CSV-файл.
        /// </summary>
        /// <param name="bankAccount">Банковский счет.</param>
        public void Visit(BankAccount bankAccount)
        {
            EnsureHeadersWritten();
            var line = $"BankAccount,{bankAccount.Id},{EscapeCsv(bankAccount.Name)},{bankAccount.Balance},,,,,,";
            _csvBuilder.AppendLine(line);
        }

        /// <summary>
        /// Сериализует данные о категории для экспорта в CSV-файл.
        /// </summary>
        /// <param name="category">Категория.</param>
        public void Visit(Category category)
        {
            EnsureHeadersWritten();
            var line = $"Category,{category.Id},{EscapeCsv(category.Name)},,{category.Type},,,,,";
            _csvBuilder.AppendLine(line);
        }

        /// <summary>
        /// Сериализует данные об операции для экспорта в CSV-файл.
        /// </summary>
        /// <param name="operation">Операция.</param>
        public void Visit(Operation operation)
        {
            EnsureHeadersWritten();
            var line = $"Operation,{operation.Id},,,{operation.Type},{operation.Amount},{operation.BankAccountId},{operation.CategoryId},{operation.Date:yyyy-MM-dd HH:mm:ss},{EscapeCsv(operation.Description ?? "")}";
            _csvBuilder.AppendLine(line);
        }

        /// <summary>
        /// Получение результата сериализации всех объектов.
        /// </summary>
        /// <returns>Все сериализованные объекты.</returns>
        public string GetResult()
        {
            return _csvBuilder.ToString();
        }

        /// <summary>
        /// Сброс накопленного результата сериализации.
        /// </summary>
        public void Reset()
        {
            _csvBuilder.Clear();
            _headersWritten = false;
        }

        /// <summary>
        /// Проверка, что заголовки напечатаны. Если не добавлены, то делаем это.
        /// </summary>
        private void EnsureHeadersWritten()
        {
            if (!_headersWritten)
            {
                _csvBuilder.AppendLine("Type,Id,Name,Balance,CategoryType,Amount,AccountId,CategoryId,Date,Description");
                _headersWritten = true;
            }
        }

        /// <summary>
        /// Сериализация строки для CSV.
        /// </summary>
        /// <param name="value">Строка.</param>
        /// <returns>Сериализованная строка.</returns>
        private string EscapeCsv(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return "";
            }

            if (value.Contains(",") || value.Contains("\"") || value.Contains("\n"))
            {
                value = value.Replace("\"", "\"\"");
                return $"\"{value}\"";
            }

            return value;
        }
    }
}
