namespace CryptographyWebApi.Application.CreateUser;

/// <summary>
/// Запрос создания пользователя
/// </summary>
/// <param name="Email">имейл пользователя</param>
/// <param name="Certificate">сертификат b64</param>
public record CreateUserQuery(string Email, string Certificate);