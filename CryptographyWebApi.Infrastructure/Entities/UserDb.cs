namespace CryptographyWebApi.Infrastructure.Entities;

public class UserDb
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Certificate { get; set; }
    
    public string Thumbprint { get; set; }
    
    public ICollection<PackageDb> OutboundPackages { get; set; } = new List<PackageDb>();
    
    public ICollection<PackageDb> InboundPackages { get; set; } = new List<PackageDb>();
}