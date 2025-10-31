using HseBank.src.Application.Facades;
using HseBank.src.Application.Services;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Application.DependencyInjection
{
    public static class ApplicationServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Фасады
            services.AddScoped<IBankAccountFacade, BankAccountFacade>();
            services.AddScoped<IOperationFacade, OperationFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<IUndoFacade, UndoFacade>();

            // Менеджер команд
            services.AddSingleton<ICommandManager, CommandManager>();

            return services;
        }
    }
}
