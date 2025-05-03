using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Packages.Common;

namespace CryptographyWebApi.Application.GetPackageById;

public class GetPackageByIdHandler
{
    private readonly IPackagesRepository _packagesRepository;

    public GetPackageByIdHandler(
        IPackagesRepository packagesRepository)
    {
        _packagesRepository = packagesRepository;
    }

    public async Task<PackageDto> HandleGetByIdAsync(Guid packageId, CancellationToken cancellationToken)
    {
        var package = await _packagesRepository.FindPackageByIdAsync(packageId, cancellationToken);
        if (package == null)
        {
            throw new Exception("Package not found");
        }

        var result = new PackageDto
        {
            Id = package.Id,
            SentDate = package.SentDate,
            CompletionDate = package.CompletionDate,
            FilePath = package.FilePath
        };
        
        return result;
    }
}