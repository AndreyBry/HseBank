namespace HseBank.src.Domain.Exceptions
{
    /// <summary>
    /// Исключение, описывающее отсутствие запрашиваемого объекта.
    /// </summary>
    public class EntityNotFoundException : Exception
    {
        public string EntityType { get; }
        public object EntityId { get; }

        /// <summary>
        /// Конструктор.
        /// </summary>
        /// <param name="entityType">Название объекта.</param>
        /// <param name="entityId">Идентификатор объекта.</param>
        public EntityNotFoundException(string entityType, object entityId)
            : base($"Не удалось найти: {entityType} {entityId}.")
        {
            EntityType = entityType;
            EntityId = entityId;
        }
    }
}