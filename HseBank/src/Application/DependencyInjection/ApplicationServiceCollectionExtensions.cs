using HseBank.src.Application.Facades;
using HseBank.src.Application.Services;
using HseBank.src.Domain.Interfaces.Facades;
using HseBank.src.Domain.Interfaces.Services;
using Microsoft.Extensions.DependencyInjection;

namespace HseBank.src.Application.DependencyInjection
{
    /// <summary>
    /// Класс, содержащий расширения для IServiceCollection.
    /// </summary>
    public static class ApplicationServiceCollectionExtensions
    {
        /// <summary>
        /// Метод-расширение для регистрации в DI фасадов и сервисов.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Фасады
            services.AddScoped<IBankAccountFacade, BankAccountFacade>();
            services.AddScoped<IOperationFacade, OperationFacade>();
            services.AddScoped<ICategoryFacade, CategoryFacade>();
            services.AddScoped<IImportFacade, ImportFacade>();
            services.AddScoped<IExportFacade, ExportFacade>();
            services.AddScoped<IUndoRedoFacade, UndoRedoFacade>();

            // Сервисы
            services.AddSingleton<ICommandManager, CommandManager>();
            services.AddSingleton<IMetricsService, MetricsService>();

            return services;
        }
    }
}
