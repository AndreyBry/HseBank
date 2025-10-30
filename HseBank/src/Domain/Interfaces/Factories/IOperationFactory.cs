using HseBank.src.Domain.DTOs;
using HseBank.src.Domain.Entities;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface IOperationFactory
    {
        public Operation Create(OperationCreateRequest request);
    }
}
