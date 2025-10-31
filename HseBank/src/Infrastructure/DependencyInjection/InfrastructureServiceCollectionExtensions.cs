using HseBank.src.Application.Services;
using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Infrastructure.Commands;
using HseBank.src.Infrastructure.Factories;
using HseBank.src.Infrastructure.Repositories.InMemory;
using HseBank.src.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            // Репозитории
            services.AddScoped<IBankAccountRepository, BankAccountRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IOperationRepository, OperationRepository>();

            // Фабрики
            services.AddScoped<IBankAccountFactory, BankAccountFactory>();
            services.AddScoped<ICategoryFactory, CategoryFactory>();
            services.AddScoped<IOperationFactory, OperationFactory>();
            services.AddScoped<ICommandFactory, CommandFactory>();

            // Валидаторы
            services.AddScoped<IOperationValidator, OperationValidator>();

            return services;
        }
    }
}
