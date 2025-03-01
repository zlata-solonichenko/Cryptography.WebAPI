using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Application.Users.Common;
using CryptographyWebApi.Infrastructure.Maps;
using Microsoft.EntityFrameworkCore;

namespace CryptographyWebApi.Infrastructure.Repositories;

public class UsersRepository : IUsersRepository
{
    private readonly CryptographyContext _context;

    public UsersRepository(CryptographyContext context)
    {
        _context = context;
    }
    public Task<bool> IsUserExistAsync(string email, CancellationToken cancellationToken)
    {
        return _context.Users.AnyAsync(u => u.Email == email.ToLower(), cancellationToken);
    }
    public Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var userDb = user.ToUserDb();
        return _context.Users.AddAsync(userDb, cancellationToken).AsTask();
    }
}