using CryptographyWebApi.Application.CreateUser;
using CryptographyWebApi.Infrastructure.Entities;

namespace CryptographyWebApi.Infrastructure.Maps;

public static class UserMap
{
    public static UserDb ToUserDb(this User user)
    {
        if (user == null)
        {
            throw new ArgumentException("Пользователь не может быть null");
        }
        var userDb = new UserDb
        {
            Id = user.Id,
            Email = user.Email,
            Certificate = user.Certificate,
            Thumbprint = user.Thumbprint,
        };
        return userDb;
    }

    public static User ToUser(this UserDb userDb)
    {
        if (userDb == null)
        {
            throw new ArgumentException("Пользователь не может быть null");
        }
          
        var user = new User(userDb.Id, userDb.Email, userDb.Certificate);
        return user;
    }
}