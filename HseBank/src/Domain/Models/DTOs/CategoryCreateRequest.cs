using HseBank.src.Domain.Enums;

namespace HseBank.src.Domain.Models.DTOs
{
    public record CategoryCreateRequest(TransactionType type, string name);
}
