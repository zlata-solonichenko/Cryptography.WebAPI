using CryptographyWebApi.Application.CreateUser;
using Microsoft.Extensions.DependencyInjection;

namespace CryptographyWebApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();
        return services;
    }
}