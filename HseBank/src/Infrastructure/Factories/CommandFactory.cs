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

        public ICommand ApplyOperationCommand(OperationApplyRequest request)
        {
            return new ApplyOperationCommand(
                request,
                _serviceProvider.GetRequiredService<IOperationFactory>(),
                _serviceProvider.GetRequiredService<IOperationRepository>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
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

        public ICommand DeleteBankAccountCommand(string name)
        {
            return new DeleteBankAccountCommand(
                name,
                _serviceProvider.GetRequiredService<IBankAccountFactory>(),
                _serviceProvider.GetRequiredService<IBankAccountRepository>()
                );
        }

        public ICommand DeleteCategoryCommand(string name)
        {
            return new DeleteCategoryCommand(
                name,
                _serviceProvider.GetRequiredService<ICategoryFactory>(),
                _serviceProvider.GetRequiredService<ICategoryRepository>()
                );
        }
    }
}
