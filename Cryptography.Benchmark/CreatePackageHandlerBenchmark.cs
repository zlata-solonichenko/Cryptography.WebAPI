using BenchmarkDotNet.Attributes;
using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Infrastructure;
using CryptographyWebApi.Infrastructure.Entities;
using CryptographyWebApi.Infrastructure.Repositories;
using CryptographyWebApi.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

namespace Cryptography.Benchmark;

[MemoryDiagnoser]
public class CreatePackageHandlerBenchmark
{
    private CreatePackageHandler _handler;
    private DtoFactory _factory;
    
    [GlobalSetup]
    public void Setup()
    {
        var dbOptions = new DbContextOptionsBuilder<CryptographyContext>()
            .UseInMemoryDatabase(databaseName: "benchmark")
            .Options;
        var dbContext = new CryptographyContext(dbOptions);
        SeedData(dbContext);
        
        var unitOfWork = new UnitOfWork(dbContext);
        var packagesRepository = new PackagesRepository(dbContext);
        var usersRepository = new UsersRepository(dbContext);
        var zipService = new ZipService();
        _handler = new CreatePackageHandler(packagesRepository, usersRepository, unitOfWork, zipService);
        _factory = new DtoFactory();
    }

    private void SeedData(CryptographyContext context)
    {
        const string userId1 = "37840abf-2229-41fb-978e-076e04be7231";
        const string userId2 = "7831e090-613d-4f96-8c84-4bf7d1d5e70a";
        
        context.Users.AddRange(
            new UserDb{ Id = Guid.Parse(userId1) },
            new UserDb{ Id = Guid.Parse(userId2) }
            );
        
        context.SaveChangesAsync();
    }

    [Benchmark]
    [Arguments("small")]
    public async Task HandleRequestAsync(string scenario)
    {
        var dto = _factory.Create(scenario);
    }
}

public class DtoFactory
{
    private readonly string _path = Path.Combine(Directory.GetCurrentDirectory(), "TestFiles");
    public CreatePackageDto Create(string scenario)
    {
        string folderPath = Path.Combine(_path, scenario);
        
        var files = Directory.GetFiles(folderPath)
            .ToDictionary(
                filePath => Path.GetFileName(filePath),
                filePath => File.ReadAllBytes(filePath)
            );
        
        return scenario switch
        {
            "small" => new CreatePackageDto(
                files, 
                Guid.NewGuid(), 
                Guid.NewGuid()),
            "medium" => new CreatePackageDto(
                files, 
                Guid.NewGuid(), 
                Guid.NewGuid()),
            "large" => new CreatePackageDto(
                files, 
                Guid.NewGuid(), 
                Guid.NewGuid())
        };
    }
}