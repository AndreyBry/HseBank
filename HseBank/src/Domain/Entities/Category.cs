using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Export;

namespace HseBank.src.Domain.Entities
{
    /// <summary>
    /// Доменный класс, описывающий категорию доходов/расходов.
    /// </summary>
    public class Category : EntityBase
    {
        private TransactionType _type;
        private string _name;

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="id">Айди.</param>
        /// <param name="type">Тип (доход/расход).</param>
        /// <param name="name">Название.</param>
        public Category(Guid id, TransactionType type, string name)
        {
            _id = id;
            _type = type;
            _name = name;
        }

        public TransactionType Type => _type;
        public string Name => _name;

        /// <summary>
        /// Метод для изменения названия.
        /// </summary>
        /// <param name="name">Новое название.</param>
        public void UpdateName(string name){
            _name = name;
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
