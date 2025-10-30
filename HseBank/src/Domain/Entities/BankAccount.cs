namespace HseBank.src.Domain.Entities
{
    public class BankAccount
    {
        private Guid _id;
        private string _name;
        private decimal _balance;

        public Guid Id => _id;
        public string Name { get => _name; set => throw new NotImplementedException(); }
        public decimal Balance { get => _balance; set => throw new NotImplementedException(); }
    }
}
