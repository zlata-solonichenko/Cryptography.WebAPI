using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Users.Common;
using CryptographyWebApi.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CryptographyWebApi.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddCryptographyInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddScoped<IUsersRepository, UsersRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<CryptographyContext>(options => options.UseNpgsql(connectionString));
        
        return services;
    }
}