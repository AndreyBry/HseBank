using HseBank.src.Domain.DTOs;

namespace HseBank.src.Domain.Interfaces.Services
{
    public interface IOperationValidator
    {
        public void Validate(OperationCreateRequest request);
    }
}
