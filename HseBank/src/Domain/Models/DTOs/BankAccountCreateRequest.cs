namespace HseBank.src.Domain.Models.DTOs
{
    /// <summary>
    /// Содержит необходимые для создания банковского счета данные.
    /// </summary>
    /// <param name="name">Название.</param>
    /// <param name="balance">Баланс.</param>
    /// <param name="id">Айди.</param>
    public record BankAccountCreateRequest(string name, decimal balance = 0m, Guid? id = null);
}
