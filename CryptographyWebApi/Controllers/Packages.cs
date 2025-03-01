using Microsoft.AspNetCore.Mvc;
using Api.Dtos;
using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.CreateUser;

namespace CryptographyWebApi.Controllers;

[ApiController]
[Route("api/packages")]
public class Packages : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> CreatePackageAsync(
        [FromBody] CreatePackagesDto  packagesDto,
        [FromServices] CreatePackageHandler handler,
        CancellationToken cancellationToken)
    {
        return Ok();
    }
}