using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Models.DTOs
{
    /// <summary>
    /// Содержит необходимые для создания категории данные.
    /// </summary>
    /// <param name="type">Тип.</param>
    /// <param name="name">Название.</param>
    /// <param name="id">Айди.</param>
    public record CategoryCreateRequest(TransactionType type, string name, Guid? id = null);
}
