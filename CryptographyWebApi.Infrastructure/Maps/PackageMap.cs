using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Infrastructure.Entities;

namespace CryptographyWebApi.Infrastructure.Maps;

public static class PackageMap
{
    public static PackageDb ToPackageDb(this Package package)
    {
        if (package == null)
        {
            throw new ArgumentException("Пакет не может быть null");
        }
        var packageDb = new PackageDb
        {
            Id = package.Id,
            //Sender = (CryptographyWebApi.Infrastructure.Entities.UserDb)package.Sender,
            SenderID = package.SenderID,
            SentDate = package.SentDate,
            CompletionDate = package.CompletionDate,
            FilePath = package.FilePath,
        };
        return packageDb;
    }

    public static Package ToPackage(this PackageDb packageDb)
    {
        if (packageDb == null)
        {
            throw new ArgumentException("Пакет не может быть null");
        } 
        
        //, packageDb.Sender
        var package = new Package(packageDb.Id, packageDb.SenderID, packageDb.SentDate, packageDb.CompletionDate, packageDb.FilePath);
        return package;
    }
}