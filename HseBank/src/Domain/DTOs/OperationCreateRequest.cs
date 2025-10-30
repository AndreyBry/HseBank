using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.DTOs
{
    public record OperationCreateRequest(TransactionType type, Guid bankAccountId, decimal amount, Guid categoryId, string? description);
}
