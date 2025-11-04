using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Export;

namespace HseBank.src.Domain.Entities
{
    /// <summary>
    /// Доменный класс, описывающий операции по счету.
    /// </summary>
    public class Operation : EntityBase
    {
        private TransactionType _type;
        private Guid _bankAccountId;
        private decimal _amount;
        private DateTime _date;
        private string? _description;
        private Guid _categoryId;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Айди.</param>
        /// <param name="type">Тип (доход/расход).</param>
        /// <param name="bankAccountId">Айди счета, по которому производится операция.</param>
        /// <param name="amount">Сумма операции.</param>
        /// <param name="date">Дата проведения операции.</param>
        /// <param name="categoryId">Айди категории, к которой относится операция.</param>
        /// <param name="description">Описание.</param>
        public Operation(Guid id, TransactionType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string? description)
        {
            _id = id;
            _type = type;
            _bankAccountId = bankAccountId;
            _amount = amount;
            _date = date;
            _description = description;
            _categoryId = categoryId;
        }

        public TransactionType Type => _type;
        public Guid BankAccountId => _bankAccountId;
        public decimal Amount => _amount;
        public DateTime Date => _date;
        public string? Description => _description;
        public Guid CategoryId => _categoryId;

        /// <summary>
        /// Метод для экспорта данных.
        /// </summary>
        /// <param name="visitor">Объект, который производит экспорт.</param>
        public void Accept(IDataVisitor visitor)
        {
            visitor.Visit(this);
        }
    }
}
