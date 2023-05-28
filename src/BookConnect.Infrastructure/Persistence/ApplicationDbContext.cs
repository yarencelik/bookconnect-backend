using BookConnect.Domain.Common;
using BookConnect.Domain.Entities;
using BookConnect.Infrastructure.Persistence.Seeds;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using BookConnect.Application.Features.Auth;
using BookConnect.Application.Features.Shelves;

namespace BookConnect.Infrastructure.Persistence;

public sealed class ApplicationDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();
    public DbSet<Author> Authors => Set<Author>();
    public DbSet<Book> Books => Set<Book>();
    public DbSet<Post> Posts => Set<Post>();
    public DbSet<Likes> Likes => Set<Likes>();
    public DbSet<Follow> Follows => Set<Follow>();
    public DbSet<Review> Reviews => Set<Review>();

    private readonly IConfiguration _config;
    private readonly IPasswordService _passwordService;
    private readonly IShelfService _shelfService;
    public ApplicationDbContext(IConfiguration config, IPasswordService passwordService, IShelfService shelfService)
    {
        _config = config;
        _passwordService = passwordService;
        _shelfService = shelfService;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(_config["ConnectionStrings:DB"]);
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        modelBuilder.AddInitialData(_config, _passwordService, _shelfService);

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var item in ChangeTracker.Entries<BaseEntity>())
        {
            if(item.State == EntityState.Added)
            {
                item.Entity.CreatedAt = DateTime.UtcNow;
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }

            if(item.State == EntityState.Modified)
            {
                item.Entity.UpdatedAt = DateTime.UtcNow;
                break;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}
