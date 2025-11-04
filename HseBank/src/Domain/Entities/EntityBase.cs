namespace HseBank.src.Domain.Entities
{
    /// <summary>
    /// Абстрактный класс, описывающий общие свойства доменных классов.
    /// </summary>
    public abstract class EntityBase
    {
        protected Guid _id;

        public Guid Id { get => _id; init => _id = value; }
    }
}
