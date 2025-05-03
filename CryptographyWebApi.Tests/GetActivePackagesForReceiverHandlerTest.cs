using System.Collections;
using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.GetActivePackages;
using CryptographyWebApi.Application.Packages.Common;
using System.Text.Json;
using CryptographyWebApi.Infrastructure.Entities;
using NSubstitute;

namespace CryptographyWebApi.Tests;

public class GetActivePackagesForReceiverHandlerTest
{
    private List<Package> _activePackages = new List<Package>(); 
    
    public GetActivePackagesForReceiverHandlerTest()
    {
        var json = File.ReadAllText("activePackages.json");
        _activePackages = JsonSerializer.Deserialize<List<Package>>(json)!;
    }
    
    [Fact]
    public async Task GetActivePackagesForReceiverHandlerTest_ReturnsIds_WhenActivePackagesExists()
    {
        // Arrange
        var recipientId = _activePackages.First().Recipient.Id;
        var cancellationToken = CancellationToken.None;

        var mockRepository = Substitute.For<IPackagesRepository>();
        mockRepository
            .GetActivePackagesForRecipientAsync(recipientId, cancellationToken)
            .Returns(Task.FromResult<IEnumerable<Package>>(_activePackages));
        
        var handler = new GetActivePackagesForReceiverHandler(mockRepository);

        // Act
        var result = await handler.HandleAsync(recipientId, cancellationToken);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_activePackages.Select(p => p.Id), result);
    }
    
    [Fact]
    public async Task GetActivePackagesForReceiverHandlerTest_ThrowsException_WhenNoActivePackagesExists()
    {
        // Arrange
        var recipientId = _activePackages.First().Recipient.Id;
        var cancellationToken = CancellationToken.None;

        var mockRepository = Substitute.For<IPackagesRepository>();
        mockRepository
            .GetActivePackagesForRecipientAsync(recipientId, cancellationToken)
            .Returns(new List<Package>());

        var handler = new GetActivePackagesForReceiverHandler(mockRepository);

        // Act and Assert
        var exception = await Assert.ThrowsAsync<Exception>(() =>
            handler.HandleAsync(recipientId, cancellationToken));

        Assert.Equal("Active packages not found", exception.Message);
    }
}