using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Application.Users.Common;
using CryptographyWebApi.Infrastructure.Repositories;
using CryptographyWebApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptographyWebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCryptographyInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IPackagesRepository, PackagesRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IZipService, ZipService>();
        services.AddDbContext<CryptographyContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}