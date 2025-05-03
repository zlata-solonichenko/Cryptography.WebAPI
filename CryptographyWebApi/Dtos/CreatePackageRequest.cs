namespace CryptographyWebApi.Dtos;

public class CreatePackageRequest
{
    public List<IFormFile> FileInfo { get; set; }

    public Guid SenderId { get; set; }

    public Guid RecipientId { get; set; }
}