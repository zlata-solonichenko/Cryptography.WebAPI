using System.IO.Compression;
using CryptographyWebApi.Application.Common;

namespace CryptographyWebApi.Infrastructure.Services;

public class ZipService : IZipService
{
    public byte[] Compress(Dictionary<string, byte[]> data)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var keyValuePair in data)
                {
                    var fileName = keyValuePair.Key;
                    var fileBytes = keyValuePair.Value;

                    var zipFile = zipArchive.CreateEntry(fileName); //создаем новый файл в архиве
                    using (var stream = zipFile.Open()) //для записи в файл внутри зипа
                    {
                        stream.Write(fileBytes, 0, fileBytes.Length);
                    }
                }
            }
            return memoryStream.ToArray();
        }
    }

    public async Task<byte[]> CompressAsync(Dictionary<string, byte[]> data, CancellationToken cancellationToken)
    {
        using (var memoryStream = new MemoryStream())
        {
            using (var zipArchive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var keyValuePair in data)
                {
                    var fileName = keyValuePair.Key;
                    var fileBytes = keyValuePair.Value;

                    var zipArchiveEntry = zipArchive.CreateEntry(fileName);
                    using (var stream = zipArchiveEntry.Open())
                    {
                        await stream.WriteAsync(fileBytes, 0, fileBytes.Length, cancellationToken);
                    }
                }
            }
            
            return memoryStream.ToArray();
        }
    }
}