using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Entities
{
    public class Operation : EntityBase
    {
        private TransactionType _type;
        private Guid _bankAccountId;
        private decimal _amount;
        private DateTime _date;
        private string? _description;
        private Guid _categoryId;

        public Operation(Guid id, TransactionType type, Guid bankAccountId, decimal amount, DateTime date, Guid categoryId, string? description)
        {
            Id = id;
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
    }
}
