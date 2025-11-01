using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface ICategoryFacade
    {
        public OperationResult Create(CategoryCreateRequest request);
        public OperationResult<IEnumerable<Category>> GetAll();
        public OperationResult<IEnumerable<Category>> GetByType(TransactionType type);
        public OperationResult ChangeName(string oldName, string newName);
        public OperationResult Delete(Guid id);
    }
}
