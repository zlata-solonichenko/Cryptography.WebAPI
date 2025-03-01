namespace CryptographyWebApi.Application.Common;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);

    /// <summary>
    /// Метод сохраняет изменения, сделанные в контексте, в базу данных
    /// </summary>
    Task SaveChangesAsync();

    /// <summary>
    /// Метод обновляет базу данных до последней версии миграций
    /// </summary>
    Task MigrateDatabaseAsync();

    /// <summary>
    /// Метод ссвобождает ресурсы, предотвращая утечки памяти
    /// </summary>
    void DisposeTask();
}