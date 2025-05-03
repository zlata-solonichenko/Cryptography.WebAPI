namespace CryptographyWebApi.Application.Common;

public interface IZipService
{
    public byte[] Compress(Dictionary<string, byte[]> data);
    
    public Task<byte[]> CompressAsync(Dictionary<string, byte[]> data, CancellationToken cancellationToken);
}