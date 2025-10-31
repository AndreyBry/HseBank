using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface ICategoryFacade
    {
        public OperationResult Create(CategoryCreateRequest request);
        public OperationResult ChangeName(string oldName, string newName);
        public OperationResult Delete(string name);
    }
}
