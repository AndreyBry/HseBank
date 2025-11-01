using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.UserInterface.Menus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace HseBank.src.UserInterface.DependencyInjection
{
    public static class UserInterfaceServiceCollectionExtensions
    {
        public static IServiceCollection AddUserInterfaceServices(this IServiceCollection services)
        {
            // Настройка логирования
            services.AddLogging(builder =>
            {
                builder.AddConsole();
                builder.SetMinimumLevel(LogLevel.Error);
            });

            // Регистрация меню
            services.AddScoped<IMenu, MainMenu>();
            services.AddScoped<IBankAccountMenu, BankAccountMenu>();
            services.AddScoped<ICategoryMenu, CategoryMenu>();
            services.AddScoped<IOperationMenu, OperationMenu>();
            services.AddScoped<IUndoRedoMenu, UndoRedoMenu>();

            return services;
        }
    }
}
