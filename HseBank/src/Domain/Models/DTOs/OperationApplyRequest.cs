using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Models.DTOs
{
    /// <summary>
    /// Содержит необходимые для создания и выполнения операции по счету данные.
    /// </summary>
    /// <param name="type">Тип.</param>
    /// <param name="bankAccountId">Айди счета, по которому проводится операция.</param>
    /// <param name="amount">Сумма операции.</param>
    /// <param name="categoryId">Категория, к которой относится операция.</param>
    /// <param name="description">Описание.</param>
    /// <param name="date">Дата проведения.</param>
    /// <param name="id">Айди.</param>
    /// <param name="needValidateBalance">Необходимость проверки баланса счета.</param>
    public record OperationApplyRequest(TransactionType type, Guid bankAccountId, decimal amount, Guid categoryId, string? description, DateTime? date = null, Guid? id = null, bool needValidateBalance = true);
}
