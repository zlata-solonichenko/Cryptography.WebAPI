using CryptographyWebApi.Application.CreatePackage;
using CryptographyWebApi.Application.Packages.Common;
using CryptographyWebApi.Infrastructure.Maps;
using Microsoft.EntityFrameworkCore;

namespace CryptographyWebApi.Infrastructure.Repositories;

public class PackagesRepository : IPackagesRepository
{
    private readonly CryptographyContext _context;

    public PackagesRepository(CryptographyContext context)
    {
        _context = context;
    }

    public async Task CreatePackageAsync(Package package, CancellationToken cancellationToken)
    {
        var packageDb = package.ToPackageDb();
        var sender = await _context.Users.FindAsync(package.Sender.Id, cancellationToken);
        var recipient = await _context.Users.FindAsync(package.Recipient.Id, cancellationToken);
        packageDb.Sender = sender;
        packageDb.Recipient = recipient;

        await _context.Packages.AddAsync(packageDb, cancellationToken);
    }

    public async Task<Package?> FindPackageByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var packageDb = await _context.Packages
            .Include(p=>p.Sender)
            .FirstOrDefaultAsync(u => u.Id == id, cancellationToken);

        if (packageDb == null)
        {
            return null;
        }

        var recipient = await _context.Users.FindAsync(packageDb.RecipientId, cancellationToken);
        packageDb.Recipient = recipient;
        
        var package = packageDb.ToPackage();

        return package;
    }

    public async Task<IEnumerable<Package>> GetPackagesForRecipientAsync(Guid recipientId,
        CancellationToken cancellationToken)
    {
        var packagesDb = await _context.Packages
            .Where(p => p.Recipient.Id == recipientId)
            .ToListAsync(cancellationToken);
        var packages = packagesDb.Select(p => p.ToPackage());

        return packages;
    }

    public async Task<IEnumerable<Package>> GetActivePackagesForRecipientAsync(Guid recipientId,
        CancellationToken cancellationToken)
    {
        var packagesDb = await _context.Packages
            .Where(p => p.Recipient.Id == recipientId && p.CompletionDate == null)
            .ToListAsync(cancellationToken);
        var packages = packagesDb.Select(p => p.ToPackage());

        return packages;
    }

    public Task CompletePackagesAsync(Guid[] ids, CancellationToken cancellationToken)
    {
        return _context.Packages
            .Where(p => ids.Contains(p.Id))
            .ForEachAsync(p => p.CompletionDate = DateTime.UtcNow, cancellationToken);
    }
}