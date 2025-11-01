using HseBank.src.Application.DependencyInjection;
using HseBank.src.Domain.Interfaces.Menus;
using HseBank.src.Infrastructure.DependencyInjection;
using HseBank.src.UserInterface.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;
        Console.InputEncoding = Encoding.UTF8;

        var serviceProvider = SetupDependencyInjection();

        var mainMenu = serviceProvider.GetRequiredService<IMenu>();
        mainMenu.Show();
    }

    static IServiceProvider SetupDependencyInjection()
    {
        var services = new ServiceCollection();
        services.AddInfrastructureServices();
        services.AddApplicationServices();
        services.AddUserInterfaceServices();
        var provider = services.BuildServiceProvider();
        return provider;
    }
}