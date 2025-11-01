using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Domain.Models.Results;

namespace HseBank.src.Domain.Interfaces.Facades
{
    public interface IOperationFacade
    {
        public OperationResult Apply(OperationApplyRequest request);
    }
}
