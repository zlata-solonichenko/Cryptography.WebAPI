using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Application.Users.Common;

namespace CryptographyWebApi.Application.DownloadPackage;

public class DownloadPackageHandler
{
    private readonly IPackagesRepository _packagesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DownloadPackageHandler(
        IPackagesRepository packagesRepository,
        IUnitOfWork unitOfWork)
    {
        _packagesRepository = packagesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<byte[]> HandleAsync(Guid packageId, CancellationToken cancellationToken)
    {
        var package = await _packagesRepository.FindPackageByIdAsync(packageId, cancellationToken);
        if (package == null)
        {
            throw new Exception("Package not found");
        }
        
        await _packagesRepository.CompletePackagesAsync([package.Id], cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        
        return File.ReadAllBytes(package.FilePath);
    }
}