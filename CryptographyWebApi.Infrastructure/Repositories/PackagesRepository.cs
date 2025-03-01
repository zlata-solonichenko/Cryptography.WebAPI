using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Infrastructure.Maps;

namespace CryptographyWebApi.Infrastructure.Repositories;

public class PackagesRepository : IPackagesRepository
{
    private readonly CryptographyContext _context;

    public PackagesRepository(CryptographyContext context)
    {
        _context = context;
    }

    public Task CreatePackageAsync(Package package, CancellationToken cancellationToken)
    {
        var packageDb = package.ToPackageDb();
        return _context.Packages.AddAsync(packageDb).AsTask();
    }
}