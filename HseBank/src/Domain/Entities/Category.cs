using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Entities
{
    public class Category : EntityBase
    {
        private TransactionType _type;
        private string _name;

        public Category(Guid id, TransactionType type, string name)
        {
            _id = id;
            _type = type;
            _name = name;
        }

        public TransactionType Type => _type;
        public string Name => _name;

        public void UpdateName(string name) { _name = name; }
    }
}
