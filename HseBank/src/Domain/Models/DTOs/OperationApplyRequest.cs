using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Models.DTOs
{
    public record OperationApplyRequest(TransactionType type, Guid bankAccountId, decimal amount, Guid categoryId, string? description);
}
