using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Entities
{
    public class Category
    {
        private Guid _id;
        private TransactionType _type;
        private string _name;

        public Guid Id => _id;
        public TransactionType Type { get => _type; set => _type = value; }
        public string Name { get => _name; set => _name = value; }
    }
}
