using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Application.Users.Common;

namespace CryptographyWebApi.Application.CreatePackage;

public class CreatePackageHandler
{
    private readonly CreatePackageHandler _packagesRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePackageHandler(CreatePackageHandler packagesRepository, IUnitOfWork unitOfWork)
    {
        _packagesRepository = packagesRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<> HandleAsync(CancellationToken cancellationToken)
    {
        
    }
}