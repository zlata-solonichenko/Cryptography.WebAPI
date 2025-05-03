using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Application.DownloadPackage;
using CryptographyWebApi.Application.GetActivePackages;
using CryptographyWebApi.Application.GetPackageById;
using Swashbuckle.AspNetCore.Annotations;

namespace CryptographyWebApi.Controllers;
//http://localhost:5035/swagger/index.html

[ApiController]
[Route("api/packages")]
public class PackagesController : ControllerBase
{
    [HttpPost("oneFile")]
    public async Task<IActionResult> CreatePackageAsync(
        IFormFile file, 
        [FromQuery] Guid senderId, 
        [FromQuery] Guid recipientId, 
        [FromServices] CreatePackageHandler handler,
        CancellationToken cancellationToken)
    {
        var dic = new Dictionary<string, byte[]>();

        using var ms = new MemoryStream();
        file.CopyTo(ms);
        dic.Add(file.FileName, ms.ToArray());
            
        var packageDto = new CreatePackageDto(dic, senderId, recipientId); 
        var result = await handler.HandleAsync(packageDto, cancellationToken);
            
        return Ok(result);
    }
    
    [HttpPost("manyFiles")]
    public async Task<IActionResult> CreatePackagesAsync(
        IFormFileCollection files,
        [FromQuery] Guid senderId, 
        [FromQuery] Guid recipientId, 
        [FromServices] CreatePackageHandler handler,
        CancellationToken cancellationToken)
    {
        var dic = new Dictionary<string, byte[]>();

        foreach (var file in files.Where(f => f.Length > 0))
        {
            using var msMany = new MemoryStream();
            await file.CopyToAsync(msMany, cancellationToken);
            dic.Add(file.FileName, msMany.ToArray());
        }
        
        var packageDto = new CreatePackageDto(dic, senderId, recipientId); 
        var result = await handler.HandleAsync(packageDto, cancellationToken);
        
        return Ok(result);
    }
    
    [HttpGet("{packageId}")]
    public async Task<IActionResult> GetPackageByIdAsync(
        [FromRoute] Guid packageId,
        [FromServices] GetPackageByIdHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleGetByIdAsync(packageId, cancellationToken);
        return Ok(result);
    }
    
    [HttpGet("{packageId}/download")]
    public async Task<IActionResult> DownloadPackageZipAsync(
        [FromRoute] Guid packageId,
        [FromServices] DownloadPackageHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(packageId, cancellationToken);
        return new NamedFileContentResult(result, "application/zip", $"package_{packageId}.zip");
    }
    
    [HttpGet("active/recipient/{recipientId}")]
    public async Task<IActionResult> GetActivePackagesForReceiverAsync(
        [FromRoute] Guid recipientId,
        [FromServices] GetActivePackagesForReceiverHandler handler,
        CancellationToken cancellationToken)
    {
        var result = await handler.HandleAsync(recipientId, cancellationToken);
        return Ok(result);
    }
}
