using HseBank.src.Application.DependencyInjection;
using HseBank.src.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

public class Program
{
    public static void Main(string[] args)
    {
        var services = CreateServiceCollection();
        var serviceProvider = services.BuildServiceProvider();
    }

    private static IServiceCollection CreateServiceCollection()
    {
        var services = new ServiceCollection();
        services.AddInfrastructureServices();
        services.AddApplicationServices();
        return services;
    }
}