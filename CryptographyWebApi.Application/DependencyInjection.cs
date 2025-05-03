using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Application.DownloadPackage;
using CryptographyWebApi.Application.GetActivePackages;
using CryptographyWebApi.Application.GetPackageById;
using Microsoft.Extensions.DependencyInjection;

namespace CryptographyWebApi.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<CreateUserHandler>();
        services.AddScoped<CreatePackageHandler>();
        services.AddScoped<DownloadPackageHandler>();
        services.AddScoped<GetActivePackagesForReceiverHandler>();
        services.AddScoped<GetPackageByIdHandler>();
        return services;
    }
}