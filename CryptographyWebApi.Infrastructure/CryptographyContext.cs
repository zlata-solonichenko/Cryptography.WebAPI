using CryptographyWebApi.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace CryptographyWebApi.Infrastructure;

public class CryptographyContext : DbContext
{
    public DbSet<UserDb> Users { get; set; }
    public DbSet<PackageDb> Packages { get; set; }
    public CryptographyContext(DbContextOptions<CryptographyContext> options) : base(options)
    {
        //Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Конфигурации моделей
        modelBuilder.Entity<UserDb>().HasKey(u => u.Id);
        
        modelBuilder.Entity<UserDb>()
            .HasMany<PackageDb>(u => u.OutboundPackages)
            .WithOne(p => p.Sender)
            .HasForeignKey(p => p.SenderId);
        
        modelBuilder.Entity<PackageDb>().HasKey(p => p.Id);
        
        modelBuilder.Entity<UserDb>()
            .HasMany<PackageDb>(u => u.InboundPackages)
            .WithOne(p => p.Recipient)
            .HasForeignKey(p => p.RecipientId);
    }
}

public class CryptographyContextFactory : IDesignTimeDbContextFactory<CryptographyContext>
{
    public CryptographyContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CryptographyContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=crypto;Username=postgres;Password=postgres");
        
        return new CryptographyContext(optionsBuilder.Options);
    }
}