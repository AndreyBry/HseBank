using HseBank.src.Domain.Interfaces.Export;

namespace HseBank.src.Domain.Entities
{
    /// <summary>
    /// Доменный класс, описывающий банковский счет.
    /// </summary>
    public class BankAccount : EntityBase
    {
        private string _name;
        private decimal _balance;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Айди.</param>
        /// <param name="name">Название.</param>
        /// <param name="balance">Баланс.</param>
        public BankAccount(Guid id, string name, decimal balance)
        {
            _id = id;
            _name = name;
            _balance = balance;
        }

        public string Name => _name;
        public decimal Balance => _balance;

        /// <summary>
        /// Метод для изменения названия.
        /// </summary>
        /// <param name="name">Новое название.</param>
        public void UpdateName(string name)
        {
            _name = name;
        }

        /// <summary>
        /// Метод для изменения баланса.
        /// </summary>
        /// <param name="delta">Сумма, на которую нужно увеличить баланс.</param>
        public void UpdateBalance(decimal delta)
        { 
            _balance += delta;
        }

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
