namespace CryptographyWebApi.Infrastructure.Entities;

public class UserDb
{
    public Guid Id { get; set; }
    
    public string Email { get; set; }
    
    public string Certificate { get; set; }
    
    public string Thumbprint { get; set; }
    
    public List<PackageDb> Packages { get; private set; } = new List<PackageDb>();
}