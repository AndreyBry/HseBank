using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface ICommandFactory
    {
        public ICommand CreateBankAccountCommand(BankAccountCreateRequest request);
        public ICommand CreateCategoryCommand(CategoryCreateRequest request);
        public ICommand ApplyOperationCommand(OperationApplyRequest request);

        public ICommand ChangeBankAccountNameCommand(string oldName, string newName);
        public ICommand ChangeCategoryNameCommand(string oldName, string newName);

        public ICommand DeleteBankAccountCommand(string name);
        public ICommand DeleteCategoryCommand(string name);
    }
}
