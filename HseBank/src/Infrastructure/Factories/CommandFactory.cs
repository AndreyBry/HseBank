using HseBank.src.Domain.Entities;
using HseBank.src.Domain.Enums;
using HseBank.src.Domain.Interfaces.Commands;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Models.DTOs;
using HseBank.src.Infrastructure.Commands;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Infrastructure.Factories
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IServiceProvider _serviceProvider;

        public CommandFactory(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public ICommand ApplyOperationCommand(OperationApplyRequest request)
        {
            return new ApplyOperationCommand(
                request,
                _serviceProvider.GetRequiredService<IOperationFactory>(),
                _serviceProvider.GetRequiredService<IOperationRepository>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public ICommand CreateBankAccountCommand(BankAccountCreateRequest request)
        {
            return new CreateBankAccountCommand(
                request,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public ICommand CreateCategoryCommand(CategoryCreateRequest request)
        {
            return new CreateCategoryCommand(
                request,
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        public IQueryCommand<IEnumerable<BankAccount>> GetAllBankAccountsCommand()
        {
            return new GetAllBankAccountsCommand(
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public IQueryCommand<IEnumerable<Category>> GetAllCategoriesCommand()
        {
            return new GetAllCategoriesCommand(
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        public IQueryCommand<IEnumerable<Category>> GetCategoriesByTypeCommand(TransactionType type)
        {
            return new GetCategoriesByTypeCommand(
                type,
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        public ICommand ChangeBankAccountNameCommand(string oldName, string newName)
        {
            return new ChangeBankAccountNameCommand(
                oldName,
                newName,
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public ICommand ChangeCategoryNameCommand(string oldName, string newName)
        {
            return new ChangeCategoryNameCommand(
                oldName,
                newName,
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }

        public ICommand DeleteBankAccountCommand(Guid id)
        {
            return new DeleteBankAccountCommand(
                id,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public ICommand DeleteCategoryCommand(Guid id)
        {
            return new DeleteCategoryCommand(
                id,
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }
    }
}
