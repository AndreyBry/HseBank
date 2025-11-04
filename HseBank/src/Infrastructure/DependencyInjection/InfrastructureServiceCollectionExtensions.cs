using HseBank.src.Domain.Interfaces.Factories;
using HseBank.src.Domain.Interfaces.Repositories;
using HseBank.src.Domain.Interfaces.Services;
using HseBank.src.Infrastructure.Factories;
using HseBank.src.Infrastructure.Repositories.InMemory;
using HseBank.src.Infrastructure.Validation;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Infrastructure.DependencyInjection
{
    /// <summary>
    /// Класс, содержащий расширения для IServiceCollection.
    /// </summary>
    public static class InfrastructureServiceCollectionExtensions
    {
        /// <summary>
        /// Метод-расширение для регистрации в DI репозиториев, фабрик и валидаторов.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>Service Collection.</returns>
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
