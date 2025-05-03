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

    public async Task<User> FindUserByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var userDb = await _context.Users.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (userDb == null)
        {
            return null;
        }
        var user = userDb.ToUser();
        return user;
    }
    public Task CreateUserAsync(User user, CancellationToken cancellationToken)
    {
        var userDb = user.ToUserDb();
        return _context.Users.AddAsync(userDb, cancellationToken).AsTask();
    }
}