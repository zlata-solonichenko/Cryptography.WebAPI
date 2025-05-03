namespace CryptographyWebApi.Application.CreatePackage;

/// <summary>
/// Dto
/// </summary>
/// <param name="Files"></param>
/// <param name="SenderId"></param>
/// <param name="RecipientId"></param>
public record CreatePackageDto(Dictionary<string, byte[]> Files, Guid SenderId, Guid RecipientId);