using HseBank.src.Domain.Interfaces.Validators;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface IOperationValidator : IValidator<OperationApplyRequest>
    {
    }
}
