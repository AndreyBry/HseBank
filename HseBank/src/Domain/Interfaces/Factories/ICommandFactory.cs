using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Models.DTOs;

namespace HseBank.src.Domain.Interfaces.Factories
{
    public interface ICommandFactory
    {
        public ICommand CreateBankAccountCommand(BankAccountCreateRequest request);
        public ICommand CreateCategoryCommand(CategoryCreateRequest request);
        public ICommand ApplyOperationCommand(OperationApplyRequest request);

        public IQueryCommand<IEnumerable<BankAccount>> GetAllBankAccountsCommand();
        public IQueryCommand<IEnumerable<Category>> GetAllCategoriesCommand();
        public IQueryCommand<IEnumerable<Category>> GetCategoriesByTypeCommand(TransactionType type);

        public ICommand ChangeBankAccountNameCommand(string oldName, string newName);
        public ICommand ChangeCategoryNameCommand(string oldName, string newName);

        public ICommand DeleteBankAccountCommand(Guid id);
        public ICommand DeleteCategoryCommand(Guid id);
    }
}
