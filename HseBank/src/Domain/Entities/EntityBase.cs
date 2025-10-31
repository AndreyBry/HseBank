namespace HseBank.src.Domain.Entities
{
    public abstract class EntityBase
    {
        protected Guid _id;

        public Guid Id { get => _id; init => _id = value; }
    }
}
