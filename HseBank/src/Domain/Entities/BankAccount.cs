namespace HseBank.src.Domain.Entities
{
    public class BankAccount : EntityBase
    {
        private string _name;
        private decimal _balance;

        public BankAccount(Guid id, string name, decimal balance)
        {
            Id = id;
            _name = name;
            _balance = balance;
        }

        public string Name => _name;
        public decimal Balance => _balance;

        public void UpdateBalance(decimal delta) { _balance += delta; }
    }
}
