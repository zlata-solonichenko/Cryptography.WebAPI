using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Application.Users.Common;

namespace CryptographyWebApi.Application.GetActivePackages;

public class GetActivePackagesForReceiverHandler
{
    private readonly IPackagesRepository _packagesRepository;

    public GetActivePackagesForReceiverHandler(
        IPackagesRepository packagesRepository)
    {
        _packagesRepository = packagesRepository;
    }

    public async Task<IEnumerable<Guid>> HandleAsync(Guid recipientId, CancellationToken cancellationToken)
    {
        var packages = await _packagesRepository.GetActivePackagesForRecipientAsync(recipientId, cancellationToken);
        if (packages == null || !packages.Any())
        {
            throw new Exception("Active packages not found");
        }
        return packages.Select(p=>p.Id);
    }
}