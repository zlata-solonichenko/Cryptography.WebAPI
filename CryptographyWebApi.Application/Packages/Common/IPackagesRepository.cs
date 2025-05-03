using CryptographyWebApi.Application.CreatePackage;

namespace CryptographyWebApi.Application.Packages.Common;

public interface IPackagesRepository
{
    public Task CreatePackageAsync(Package package, CancellationToken cancellationToken);

    public Task<Package?> FindPackageByIdAsync(Guid id, CancellationToken cancellationToken);

    public Task<IEnumerable<Package>> GetPackagesForRecipientAsync(Guid recipientId,
        CancellationToken cancellationToken);
    
    public Task<IEnumerable<Package>> GetActivePackagesForRecipientAsync(Guid recipientId,
        CancellationToken cancellationToken);
    
    public Task CompletePackagesAsync(Guid[] ids, CancellationToken cancellationToken);
}