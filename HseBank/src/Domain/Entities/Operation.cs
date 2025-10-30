using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Entities
{
    public class Operation
    {
        private Guid _id;
        private Guid _bank_account_id;
        private TransactionType _type;
        private Guid _category_id;
        private decimal _amount;
        private DateTime _date;
        private string? _description;

        public Guid Id => _id;
        public Guid BankAccountId => _bank_account_id; 
        public TransactionType Type => _type;
        public Guid CategoryId => _category_id;
        public decimal Amount => _amount;
        public DateTime Date => _date;
        public string? Description => _description;
    }
}
