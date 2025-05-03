using CryptographyWebApi.Application.Common;
using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.DownloadPackage;
using CryptographyWebApi.Application.Packages.Common;
using NSubstitute;

namespace CryptographyWebApi.Tests;

public class DownloadPackageHandlerTest
{
    [Fact]
    public async Task DownloadPackageHandler_()
    {
        // Arrange
        var packageId = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;
        
    }
    
    [Fact]
    public async Task DownloadPackageHandler_ThrowsException_WhenPackageNotFound()
    {
        // Arrange
        var packageId = Guid.NewGuid();
        var cancellationToken = CancellationToken.None;

        var mockPackagesRepo = Substitute.For<IPackagesRepository>();
        var mockUnitOfWork = Substitute.For<IUnitOfWork>();

        mockPackagesRepo
            .FindPackageByIdAsync(packageId, cancellationToken)
            .Returns((Package)null!);

        var handler = new DownloadPackageHandler(mockPackagesRepo, mockUnitOfWork);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<Exception>(() => handler.HandleAsync(packageId, cancellationToken));
        Assert.Equal("Package not found", exception.Message);
    }
}