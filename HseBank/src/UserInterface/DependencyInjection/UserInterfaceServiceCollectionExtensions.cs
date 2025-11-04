using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.UserInterface.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HseBank.src.UserInterface.DependencyInjection
{
    /// <summary>
    /// Класс, содержащий расширения для IServiceCollection.
    /// </summary>
    public static class UserInterfaceServiceCollectionExtensions
    {
        /// <summary>
        /// Метод-расширение для регистрации в DI логгера и меню.
        /// </summary>
        /// <param name="services">Service Collection.</param>
        /// <returns>Service Collection.</returns>
        public static IServiceCollection AddUserInterfaceServices(this IServiceCollection services)
        {
            // Настройка логирования
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Error);
            });

            // Регистрация меню
            services.AddTransient<IMenu, MainMenu>();
            services.AddTransient<IBankAccountMenu, BankAccountMenu>();
            services.AddTransient<ICategoryMenu, CategoryMenu>();
            services.AddTransient<IOperationMenu, OperationMenu>();
            services.AddTransient<IMetricsMenu, MetricsMenu>();
            services.AddTransient<IImportMenu, ImportMenu>();
            services.AddTransient<IExportMenu, ExportMenu>();
            services.AddTransient<IUndoRedoMenu, UndoRedoMenu>();

            return services;
        }
    }
}
