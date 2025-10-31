public class EntityNotFoundException : Exception
{
    public string EntityType { get; }
    public object EntityId { get; }

    public EntityNotFoundException(string entityType, object entityId)
        : base($"Не удалось найти: {entityType} {entityId}.")
    {
        EntityType = entityType;
        EntityId = entityId;
    }
}