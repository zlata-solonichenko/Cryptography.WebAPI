using System.Text;
using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Application.Users.Common;
using BenchmarkDotNet.Attributes;

namespace CryptographyWebApi.Application.CreatePackage;

public class CreatePackageHandler
{
    private const string UPLOAD_DIRECTORY = "Archives";
    private readonly IPackagesRepository _packagesRepository;
    private readonly IUsersRepository _usersRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IZipService _zipService;

    public CreatePackageHandler(
        IPackagesRepository packagesRepository, 
        IUsersRepository usersRepository, 
        IUnitOfWork unitOfWork,
        IZipService zipService)
    {
        _packagesRepository = packagesRepository;
        _usersRepository = usersRepository;
        _unitOfWork = unitOfWork;
        _zipService = zipService;
    }

    public async Task<CreatePackageResult> HandleAsync(CreatePackageDto packageDto, CancellationToken cancellationToken)
    {
        //Достать сендера из репозитория юзера
        var sender = await _usersRepository.FindUserByIdAsync(packageDto.SenderId, cancellationToken);
        if (sender == null)
        {
            throw new InvalidOperationException("Sender not found");
        }
        //Достать получателя из репо юзера
        var recipient = await _usersRepository.FindUserByIdAsync(packageDto.RecipientId, cancellationToken);
        if (recipient == null)
        {
            throw new InvalidOperationException("Recipient not found");
        }
        
        var arhiveZipBytes = _zipService.Compress(packageDto.Files);
        var zipPath = Path.Combine(UPLOAD_DIRECTORY, Guid.NewGuid() + ".zip");
        File.WriteAllBytes(zipPath, arhiveZipBytes);
        
        var sendDate = DateTime.UtcNow;
        var newPackage = new Package(sender, recipient, sendDate, zipPath);
        
        //Вызвать метод добавления пакета
        await _packagesRepository.CreatePackageAsync(newPackage, cancellationToken);
        //Юнит сохранить изменения
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        //вернуть айди
        return new CreatePackageResult(newPackage.Id);

    }
}