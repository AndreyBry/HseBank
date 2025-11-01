using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface IBankAccountFacade
    {
        public OperationResult Create(BankAccountCreateRequest request);
        public OperationResult<IEnumerable<BankAccount>> GetAll();
        public OperationResult ChangeName(string oldName, string newName);
        public OperationResult Delete(Guid id);
    }
}
