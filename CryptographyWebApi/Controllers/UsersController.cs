using Api.Dtos;
using CryptographyWebApi.Application.CreateUser;
using Microsoft.AspNetCore.Mvc;

namespace CryptographyWebApi.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    [HttpPost("new")]
    public async Task<IActionResult> CreateUserAsync(
        [FromBody] CreateUserDto userDto,
        [FromServices] CreateUserHandler handler,
        CancellationToken cancellationToken)
    {
        //1) создать query
        //2) у handler вызвать метод обработки и передать query
        //3) получить результаты
        var query = new CreateUserQuery(userDto.Email, userDto.Certificate);
        
        var result = await handler.HandleAsync(query, cancellationToken);
        return Ok(result);
    }
}
