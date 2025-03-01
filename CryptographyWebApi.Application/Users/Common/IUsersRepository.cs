using CryptographyWebApi.Application.CreateUser;

namespace CryptographyWebApi.Application.Users.Common;

public interface IUsersRepository
{
    public Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken);

    public Task CreateUserAsync(User user, CancellationToken cancellationToken);
}