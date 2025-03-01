using CryptographyWebApi.Application.CreatePackage;

namespace CryptographyWebApi.Application.Packages.Common;

public interface IPackagesRepository
{
    public Task CreatePackageAsync(Package package, CancellationToken cancellationToken);
}