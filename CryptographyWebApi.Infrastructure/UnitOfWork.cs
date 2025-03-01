using CryptographyWebApi.Application.Common;
using Microsoft.EntityFrameworkCore;

namespace CryptographyWebApi.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly CryptographyContext _cryptoContext;

    public UnitOfWork(CryptographyContext shopContext)
    {
        _cryptoContext = shopContext;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return _cryptoContext.SaveChangesAsync(cancellationToken);
    }

    /// <summary>
    /// Метод сохраняет изменения, сделанные в контексте, в базу данных
    /// </summary>
    public Task SaveChangesAsync()
    {
        return _cryptoContext.SaveChangesAsync();
    }

    /// <summary>
    /// Метод обновляет базу данных до последней версии миграций
    /// </summary>
    public Task MigrateDatabaseAsync()
    {
        return _cryptoContext.Database.MigrateAsync();
    }

    /// <summary>
    /// Метод ссвобождает ресурсы, предотвращая утечки памяти
    /// </summary>
    public void DisposeTask()
    {
        _cryptoContext.Dispose();
    }
}